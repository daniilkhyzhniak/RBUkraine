using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using RBUkraine.BLL.Contracts;
using RBUkraine.PL.ViewModels.Cart;
using RBUkraine.PL.ViewModels.Products;

namespace RBUkraine.PL.Controllers
{
    [Route("cart")]
    public class CartController : Controller
    {
        private const string CookieCartName = "cart";

        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public CartController(
            IProductService productService, 
            IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            var cart = GetCart();
            var products = await _productService.GetAll(cart.Ids);
            return View(_mapper.Map<IEnumerable<ProductViewModel>>(products));
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromQuery] int productId)
        {
            var product = await _productService.Get(productId);

            if (product is null)
            {
                return NotFound();
            }

            AddToCookieCart(productId);
            return Ok();
        }

        [HttpPost("remove")]
        public IActionResult RemoveFromCart([FromQuery] int productId)
        {
            RemoveFromCookieCart(productId);
            return Ok();
        }

        [NonAction]
        private CookieCartModel GetCart()
        {
            var cookieCart = HttpContext.Request.Cookies[CookieCartName];
            return string.IsNullOrWhiteSpace(cookieCart)
                ? new CookieCartModel()
                : JsonConvert.DeserializeObject<CookieCartModel>(cookieCart);
        }

        [NonAction]
        private void AddToCookieCart(int id)
        {
            var cart = GetCart() ?? new CookieCartModel();
            cart.Ids.Add(id);
            HttpContext.Response.Cookies.Append(CookieCartName, JsonConvert.SerializeObject(cart));
        }

        [NonAction]
        private void RemoveFromCookieCart(int id)
        {
            var cart = GetCart() ?? new CookieCartModel();
            cart.Ids.Remove(id);
            HttpContext.Response.Cookies.Append(CookieCartName, JsonConvert.SerializeObject(cart));
        }
    }
}
