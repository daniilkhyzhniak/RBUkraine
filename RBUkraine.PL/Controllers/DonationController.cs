using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RBUkraine.BLL.Contracts;
using RBUkraine.BLL.Enums;
using RBUkraine.BLL.Models.CharitableOrganization;
using RBUkraine.BLL.Models.Donation;
using RBUkraine.PL.Services;
using RBUkraine.PL.ViewModels.Donation;
using Stripe.Checkout;

namespace RBUkraine.PL.Controllers
{
    [Route("donation")]
    public class DonationController : Controller
    {
        private readonly IDonationService _donationService;
        private readonly IAnimalService _animalService;
        private readonly ICharitableOrganizationService _charitableOrganizationService;
        private readonly IBonusService _bonusService;
        private readonly SessionService _sessionService;

        public DonationController(
            IDonationService donationService, 
            IAnimalService animalService, 
            ICharitableOrganizationService charitableOrganizationService,
            IBonusService bonusService)
        {
            _donationService = donationService;
            _animalService = animalService;
            _charitableOrganizationService = charitableOrganizationService;
            _bonusService = bonusService;
            _sessionService = new SessionService();
        }

        [HttpGet("rating")]
        public IActionResult Rating()
        {
            return View(_donationService.GetRating());
        }

        [HttpGet, Authorize(Roles = Roles.User)]
        public async Task<IActionResult> Donate()
        {
            var model = new DonationViewModel
            {
                Fonds = (await _charitableOrganizationService.GetAllAdmin(new CharitableOrganizationFilterModel(), CultureInfo.CurrentCulture.Name))
                    .Select(x => new SelectListItem(x.Name, x.Id.ToString())),
                Species = (await _animalService.GetAllAsync(CultureInfo.CurrentCulture.Name))
                    .Select(x => new SelectListItem(x.Species, x.Id.ToString()))
            };

            return View(model);
        }

        [HttpPost, Authorize(Roles = Roles.User)]
        public async Task<IActionResult> Donate(DonationViewModel model)
        {
            var successUrl = $"https://localhost:5001/donation/success?sessionId={{CHECKOUT_SESSION_ID}}&amount={model.Amount}";
            SessionLineItemOptions item;

            if (model.AnimalId is not null)
            {
                successUrl += $"&animalId={model.AnimalId}";
                var animal = await _animalService.GetByIdAsync(model.AnimalId.Value);
                item = new SessionLineItemOptions
                {
                    Name = animal.Species,
                    Description = animal.Description,
                    Amount = Convert.ToInt64(model.Amount) * 100,
                    Currency = "UAH",
                    Quantity = 1
                };
            } 
            else if (model.CharitableOrganizationId is not null)
            {
                successUrl += $"&charitableOrganizationId={model.CharitableOrganizationId}";
                var charitableOrganization =
                    await _charitableOrganizationService.GetByIdAsync(model.CharitableOrganizationId.Value);
                item = new SessionLineItemOptions
                {
                    Name = charitableOrganization.Name,
                    Description = charitableOrganization.Description,
                    Amount = Convert.ToInt64(model.Amount) * 100,
                    Currency = "UAH",
                    Quantity = 1
                };
            }
            else
            {
                item = new SessionLineItemOptions
                {
                    Name = "Common Donation",
                    Amount = Convert.ToInt64(model.Amount) * 100,
                    Currency = "UAH",
                    Quantity = 1
                };
            }

            var session = await _sessionService.CreateAsync(new SessionCreateOptions
            {
                SuccessUrl = successUrl,
                CancelUrl = "https://localhost:5001",
                PaymentMethodTypes = new List<string> {
                    "card",
                },
                LineItems = new List<SessionLineItemOptions> {
                    item
                },
                CustomerEmail = User.Claims.First(x => x.Type == ClaimTypes.Email).Value,
                Locale = CultureInfo.CurrentCulture.Name == Culture.Ukrainian
                    ? Culture.English
                    : CultureInfo.CurrentCulture.Name
            });
            ViewBag.SessionId = session.Id;

            return View("Checkout");
        }

        [HttpGet("success"), Authorize(Roles = Roles.User)]
        public async Task<IActionResult> DonateSuccess(
            [FromQuery] DonationViewModel model,
            [FromQuery] string sessionId)
        {
            var session = await _sessionService.GetAsync(sessionId);

            if (session?.PaymentStatus != PaymentStatus.Paid)
            {
                return BadRequest();
            }

            await _donationService.Create(new DonationModel
            {
                CharitableOrganizationId = model.CharitableOrganizationId,
                AnimalId = model.AnimalId,
                Amount = model.Amount,
                UserId = Convert.ToInt32(User.Claims.First(x => x.Type == "Id").Value)
            });

            await _bonusService.SendBonus(User.Claims.First(x => x.Type == ClaimTypes.Email).Value);

            return RedirectToAction("GetAnimals", "CharitableOrganizations");
        }
    }
}