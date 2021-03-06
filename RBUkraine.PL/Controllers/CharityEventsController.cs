using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RBUkraine.BLL.Contracts;
using RBUkraine.BLL.Enums;
using RBUkraine.BLL.Models.CharityEvent;
using RBUkraine.PL.EmailSender;
using RBUkraine.PL.EmailSender.Models;
using RBUkraine.PL.ViewModels.CharityEvents;
using Stripe.Checkout;

namespace RBUkraine.PL.Controllers
{
    [Route("events")]
    public class CharityEventsController : Controller
    {
        private readonly ICharityEventService _charityEventService;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly IUserService _userService;
        private readonly SessionService _sessionService;
        
        public CharityEventsController(
            ICharityEventService charityEventService,
            IMapper mapper,
            IEmailSender emailSender,
            IUserService userService)
        {
            _charityEventService = charityEventService;
            _mapper = mapper;
            _emailSender = emailSender;
            _userService = userService;
            _sessionService = new SessionService();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var charityEvents = await _charityEventService.GetAllAsync(CultureInfo.CurrentCulture.Name);
            return View(_mapper.Map<IEnumerable<CharityEventViewModel>>(charityEvents));
        }

        [HttpGet("admin"), Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllAdmin(CharityEventFilterModel filter)
        {
            
            var charityEvents = await _charityEventService.GetAllAsync(filter, CultureInfo.CurrentCulture.Name);
            var model = new CharityEventListViewModel
            {
                CharityEvents = _mapper.Map<IList<CharityEventViewModel>>(charityEvents),
                Filter = filter,
                
            };
            return View("GetAllAdmin", model);
        }

        [HttpGet("create"), Authorize(Roles = Roles.Admin)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create"), Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Create(
            [FromForm] CharityEventEditorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var charityEvent = _mapper.Map<CharityEventEditorModel>(model);
            await _charityEventService.CreateAsync(charityEvent);

            return RedirectToAction("GetAllAdmin");
        }

        [HttpGet("{id}/edit"), Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Update(
            [FromRoute] int id)
        {
            var charityEvent = await _charityEventService.GetByIdAsync(id);
            return View(_mapper.Map<CharityEventEditorViewModel>(charityEvent));
        }

        [HttpPost("{id}/edit"), Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Update(
            [FromRoute] int id,
            [FromForm] CharityEventEditorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var charityEvent = _mapper.Map<CharityEventEditorModel>(model);
            await _charityEventService.UpdateAsync(id, charityEvent);

            return RedirectToAction("GetAllAdmin");
        }

        [HttpPost("{id:int}/delete"), Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(
            [FromRoute] int id)
        {
            await _charityEventService.DeleteAsync(id);
            return RedirectToAction("GetAllAdmin");
        }

        [HttpPost("{id:int}/payment"), Authorize(Roles = Roles.User)]
        public async Task<IActionResult> Checkout(int id)
        {
            var charityEvent = await _charityEventService.GetByIdAsync(id, CultureInfo.CurrentCulture.Name);

            if (charityEvent is null)
            {
                return NotFound();
            }

            var session = await _sessionService.CreateAsync(new SessionCreateOptions
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
                CustomerEmail = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value,
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
            var session = await _sessionService.GetAsync(sessionId);
            
            if (session.PaymentStatus != PaymentStatus.Paid)
            {
                return BadRequest();
            }

            var user = await _userService.GetUserByEmailAsync(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value);
            var charityEvent = await _charityEventService.GetByIdAsync(id);
            await _charityEventService.AddPurchase(id, user.Id);
            
            await _emailSender.SendEmailAsync(new EmailModel
            {
                Email = user.Email,
                Subject = "RBUkraine. Покупка билета",
                Message = $@"<p>Вы купили билет на мероприятие {charityEvent.Name}.</p>
                            <p>Имя пользователя: {(string.IsNullOrWhiteSpace(user.Nickname) ? user.Email : user.Nickname)}</p>
                            <p>Email пользователя: {user.Email}</p>
                            <p>Предъявите это письмо на мероприятии.</p>"
            });

            return RedirectToAction("GetAll");
        }
    }
}
