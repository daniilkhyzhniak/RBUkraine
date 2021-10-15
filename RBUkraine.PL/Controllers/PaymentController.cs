using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

namespace RBUkraine.PL.Controllers
{
    public class PaymentController : Controller
    {
        private readonly SessionService _service;

        public PaymentController()
        {
            _service = new SessionService();
        }

        [HttpGet("checkout")]
        public async Task<IActionResult> Checkout()
        {
            var session = await _service.CreateAsync(new SessionCreateOptions
            {
                SuccessUrl = "https://localhost:5001/checkout/success?sessionId={CHECKOUT_SESSION_ID}",
                CancelUrl = "https://localhost:5001/animals",
                PaymentMethodTypes = new List<string> {
                    "card",
                },
                LineItems = new List<SessionLineItemOptions> {
                    new SessionLineItemOptions {
                        Name = "Charity Payment",
                        Amount = 1500,
                        Currency = "UAH",
                        Quantity = 1
                    }
                },
                CustomerEmail = "daniil.khyzhniak@nure.ua",
                Locale = CultureInfo.CurrentCulture.Name
            });

            ViewBag.SessionId = session.Id;

            return View();
        }

        [HttpGet("checkout/success")]
        public async Task<IActionResult> Checkout(
            [FromQuery] string sessionId)
        {
            var session = await _service.GetAsync(sessionId);
            return Ok(sessionId);
        }
    }
}
