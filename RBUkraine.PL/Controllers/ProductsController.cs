using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RBUkraine.BLL.Contracts;
using RBUkraine.BLL.Enums;
using RBUkraine.BLL.Models.Product;
using RBUkraine.PL.ViewModels.Products;

namespace RBUkraine.PL.Controllers
{
    [Route("shop")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(
            IProductService productService,
            IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] ProductFilterModel filter)
        {
            var products = await _productService.GetAll(filter, CultureInfo.CurrentCulture.Name);
            var categories = await _productService.GetCategories();
            return View(new ProductListViewModel
            {
                Products = _mapper.Map<IEnumerable<ProductViewModel>>(products),
                Filter = filter,
                CategoriesSelectList = categories.Select(category => new SelectListItem(category, category, filter.Categories.Contains(category)))
            });
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.Get(id, CultureInfo.CurrentCulture.Name);

            if (product is null)
            {
                return NotFound();
            }

            return View(_mapper.Map<ProductViewModel>(product));
        }

        [HttpGet("admin"), Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllAdmin(
            [FromQuery] ProductFilterModel filter)
        {
            var products = await _productService.GetAll(filter, CultureInfo.CurrentCulture.Name);
            var categories = await _productService.GetCategories();
            return View(new ProductListViewModel
            {
                Products = _mapper.Map<IEnumerable<ProductViewModel>>(products),
                Filter = filter,
                CategoriesSelectList = categories.Select(category => new SelectListItem(category, category, filter.Categories.Contains(category)))
            });
        }

        [HttpGet("create"), Authorize(Roles = Roles.Admin)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create"), Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Create(ProductEditorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _productService.Create(_mapper.Map<ProductEditorModel>(model));
            return RedirectToAction("GetAllAdmin");
        }

        [HttpGet("{id:int}/edit"), Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Update(int id)
        {
            var charitableOrganization = await _productService.Get(id, CultureInfo.CurrentCulture.Name);

            if (charitableOrganization is null)
            {
                return NotFound();
            }

            return View(_mapper.Map<ProductEditorViewModel>(charitableOrganization));
        }

        [HttpPost("{id:int}/edit"), Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Update(int id, ProductEditorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _productService.Update(id, _mapper.Map<ProductEditorModel>(model));
            return RedirectToAction("GetAllAdmin");
        }

        [HttpPost("{id:int}/delete")]
        public async Task<IActionResult> Delete(
            [FromRoute] int id)
        {
            await _productService.Delete(id);
            return RedirectToAction("GetAllAdmin");
        }
    }
}
