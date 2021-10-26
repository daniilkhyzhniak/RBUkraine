using Microsoft.AspNetCore.Mvc;

namespace RBUkraine.PL.Controllers
{
    public class ErrorsController : Controller
    {
        [HttpGet("access-denied")]
        public IActionResult AccessDenied()
        {
            return Forbid();
        }
    }
}
