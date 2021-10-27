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
    }
}
