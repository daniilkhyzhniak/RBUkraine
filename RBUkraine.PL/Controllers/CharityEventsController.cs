using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RBUkraine.BLL.Contracts;
using RBUkraine.BLL.Enums;
using RBUkraine.PL.ViewModels.CharityEvents;
using Stripe.Checkout;

namespace RBUkraine.PL.Controllers
{
    [Route("events")]
    public class CharityEventsController : Controller
    {
        private readonly ICharityEventService _charityEventService;
        private readonly IMapper _mapper;
        private readonly SessionService _service;

        public CharityEventsController(
            ICharityEventService charityEventService,
            IMapper mapper)
        {
            _charityEventService = charityEventService;
            _mapper = mapper;
            _service = new SessionService();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var charityEvents = await _charityEventService.GetAllAsync(CultureInfo.CurrentCulture.Name);
            return View(_mapper.Map<IEnumerable<CharityEventViewModel>>(charityEvents));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var charityEvent = await _charityEventService.GetByIdAsync(id, CultureInfo.CurrentCulture.Name);

            if (charityEvent is null)
            {
                return NotFound();
            }

            return View(_mapper.Map<CharityEventViewModel>(charityEvent));
        }

        [HttpPost("{id:int}/payment"), Authorize(Roles = Roles.User)]
        public async Task<IActionResult> Checkout(int id)
        {
            var charityEvent = await _charityEventService.GetByIdAsync(id, CultureInfo.CurrentCulture.Name);

            if (charityEvent is null)
            {
                return NotFound();
            }

            var session = await _service.CreateAsync(new SessionCreateOptions
            {
                SuccessUrl = $"https://localhost:5001/events/{id}/payment/success?sessionId={{CHECKOUT_SESSION_ID}}",
                CancelUrl = "https://localhost:5001/events",
                PaymentMethodTypes = new List<string> {
                    "card",
                },
                LineItems = new List<SessionLineItemOptions> {
                    new () {
                        Name = charityEvent.Name,
                        Description = charityEvent.Description,
                        Amount = Convert.ToInt64(charityEvent.Price) * 100,
                        Currency = "UAH",
                        Quantity = 1
                    }
                },
                CustomerEmail = User.Identity.Name,
                Locale = CultureInfo.CurrentCulture.Name == Culture.Ukrainian 
                    ? Culture.English
                    : CultureInfo.CurrentCulture.Name
            });

            ViewBag.SessionId = session.Id;

            return View("Checkout");
        }

        [HttpGet("{id}/payment/success"), Authorize(Roles = Roles.User)]
        public async Task<IActionResult> CheckoutSuccess(
            int id,
            [FromQuery] string sessionId)
        {
            var session = await _service.GetAsync(sessionId);

            return Ok(sessionId);
        }
    }
}
