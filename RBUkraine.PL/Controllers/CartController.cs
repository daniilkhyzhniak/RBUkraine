using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using RBUkraine.BLL.Contracts;
using RBUkraine.BLL.Enums;
using RBUkraine.BLL.Models.Order;
using RBUkraine.PL.ViewModels.Cart;
using RBUkraine.PL.ViewModels.Products;
using Stripe.Checkout;

namespace RBUkraine.PL.Controllers
{
    [Route("cart")]
    public class CartController : Controller
    {
        private const string CookieCartName = "cart";

        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly SessionService _sessionService;

        public CartController(
            IProductService productService,
            IOrderService orderService,
            IMapper mapper)
        {
            _productService = productService;
            _orderService = orderService;
            _mapper = mapper;
            _sessionService = new SessionService();
        }

        [HttpGet, Authorize(Roles = Roles.User)]
        public async Task<IActionResult> Cart()
        {
            var cartItems = GetCart().Items;
            var products = await _productService.GetAll(cartItems.Select(x => x.Id));
            var model = new CartViewModel
            {
                Products = products.Select(product => new ProductCartViewModel
                {
                    Product = _mapper.Map<ProductViewModel>(product),
                    Amount = cartItems.First(x => x.Id == product.Id).Amount
                }),
                Sum = products.Select(product => new { Price = product.Price * cartItems.First(item => item.Id == product.Id).Amount }).Sum(x => x.Price)
            };
            return View(model);
        }

        [HttpPost("add"), Authorize(Roles = Roles.User)]
        public async Task<IActionResult> AddToCart([FromQuery] int id)
        {
            var product = await _productService.Get(id);

            if (product is null)
            {
                return NotFound();
            }

            AddToCookieCart(id);
            return Ok();
        }
        
        [HttpPost("increase-amount"), Authorize(Roles = Roles.User)]
        public async Task<IActionResult> IncreateProductAmount([FromQuery] int id)
        {
            var product = await _productService.Get(id);

            if (product is null)
            {
                return NotFound();
            }

            ChangeAmountForCookieCartItem(id, 1);

            var cartItems = GetCart().Items;
            var products = await _productService.GetAll(cartItems.Select(x => x.Id));
            var sum = products.Select(product => new { Price = product.Price * cartItems.First(item => item.Id == product.Id).Amount }).Sum(x => x.Price);

            return Ok(sum);
        }

        [HttpPost("decrease-amount"), Authorize(Roles = Roles.User)]
        public async Task<IActionResult> DecreaseProductAmount([FromQuery] int id)
        {
            var product = await _productService.Get(id);

            if (product is null)
            {
                return NotFound();
            }

            ChangeAmountForCookieCartItem(id, -1);

            var cartItems = GetCart().Items;
            var products = await _productService.GetAll(cartItems.Select(x => x.Id));
            var sum = products.Select(product => new { Price = product.Price * cartItems.First(item => item.Id == product.Id).Amount }).Sum(x => x.Price);

            return Ok(sum);
        }

        [HttpPost("remove"), Authorize(Roles = Roles.User)]
        public async Task<IActionResult> RemoveFromCart([FromQuery] int productId)
        {
            RemoveFromCookieCart(productId);

            var cartItems = GetCart().Items;
            var products = await _productService.GetAll(cartItems.Select(x => x.Id));
            var sum = products.Select(product => new { Price = product.Price * cartItems.First(item => item.Id == product.Id).Amount }).Sum(x => x.Price);

            return Ok(sum);
        }

        [HttpPost("clear"), Authorize(Roles = Roles.User)]
        public IActionResult ClearCart()
        {
            RemoveAllFromCookieCart();
            return Ok();
        }

        [HttpPost("~/order"), Authorize(Roles = Roles.User)]
        public async Task<IActionResult> MakeOrder()
        {
            var cart = GetCart();
            var products = await _productService.GetAll(cart.Items.Select(x => x.Id));
            var model = new MakeOrderViewModel
            {
                Products = _mapper.Map<IEnumerable<ProductViewModel>>(products).Select(product => new ProductCartViewModel
                {
                    Product = product,
                    Amount = cart.Items.First(x => x.Id == product.Id).Amount
                }),
                Sum = products.Select(product => new { Price = product.Price * cart.Items.First(item => item.Id == product.Id).Amount }).Sum(x => x.Price)
            };
            return View(model);
        }

        [HttpPost("~/order"), Authorize(Roles = Roles.User)]
        public async Task<IActionResult> MakeOrder(MakeOrderViewModel model)
        {
            var cart = GetCart();
            var products = _mapper.Map<IEnumerable<ProductViewModel>>(await _productService.GetAll(cart.Items.Select(x => x.Id)));

            if (!ModelState.IsValid)
            {
                model.Products = _mapper.Map<IEnumerable<ProductViewModel>>(products).Select(product => new ProductCartViewModel
                {
                    Product = product,
                    Amount = cart.Items.First(x => x.Id == product.Id).Amount
                });
                model.Sum = products.Select(product => new { Price = product.Price * cart.Items.First(item => item.Id == product.Id).Amount }).Sum(x => x.Price);
                return View(model);
            }

            var session = await _sessionService.CreateAsync(new SessionCreateOptions
            {
                SuccessUrl = "https://localhost:5001/order/success",
                CancelUrl = "https://localhost:5001",
                PaymentMethodTypes = new List<string> {
                    "card",
                },
                LineItems = products.Select(product => new SessionLineItemOptions
                {
                    Name = product.Name,
                    Description = product.Description,
                    Images = new List<string> { product.Image.Url },
                    Price = $"{product.Price:CO}",
                    Currency = "UAH",
                    Quantity = 1,
                    Amount = Convert.ToInt64(product.Price) * 100
                }).ToList(),
                CustomerEmail = User.Claims.First(x => x.Type == ClaimTypes.Email).Value,
                Locale = CultureInfo.CurrentCulture.Name == Culture.Ukrainian
                    ? Culture.English
                    : CultureInfo.CurrentCulture.Name
            });

            ViewBag.SessionId = session.Id;
            return View("Checkout");
        }

        [HttpGet("~/order/success"), Authorize(Roles = Roles.User)]
        public async Task<IActionResult> OrderSuccess(
            [FromQuery] string sessionId)
        {
            var session = await _sessionService.GetAsync(sessionId);

            if (session?.PaymentStatus != PaymentStatus.Paid)
            {
                return BadRequest();
            }

            var cart = GetCart();
            await _orderService.MakeOrder(new OrderCreationModel
            {
                UserId = Convert.ToInt32(User.Claims.First(x => x.Type == "Id")),
                ProductsIds = cart.Items.Select(x => x.Id)
            });

            return RedirectToAction("GetAnimals", "CharitableOrganizations");
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
            var item = cart.Items.FirstOrDefault(x => x.Id == id);

            if (item is not null)
            {
                item.Amount += 1;
            }
            else
            {
                cart.Items.Add(new CookieCartItemModel
                {
                    Id = id,
                    Amount = 1
                });
            }

            HttpContext.Response.Cookies.Append(CookieCartName, JsonConvert.SerializeObject(cart));
        }

        [NonAction]
        private void ChangeAmountForCookieCartItem(int id, int amountToAdd)
        {
            var cart = GetCart() ?? new CookieCartModel();
            var item = cart.Items.FirstOrDefault(x => x.Id == id);

            if (item is not null)
            {
                item.Amount += amountToAdd;
            }

            HttpContext.Response.Cookies.Append(CookieCartName, JsonConvert.SerializeObject(cart));
        }

        [NonAction]
        private void RemoveFromCookieCart(int id)
        {
            var cart = GetCart() ?? new CookieCartModel();
            var item = cart.Items.FirstOrDefault(x => x.Id == id);
            cart.Items.Remove(item);
            HttpContext.Response.Cookies.Append(CookieCartName, JsonConvert.SerializeObject(cart));
        }
        
        [NonAction]
        private void RemoveAllFromCookieCart()
        {
            HttpContext.Response.Cookies.Delete(CookieCartName);
        }
    }
}
