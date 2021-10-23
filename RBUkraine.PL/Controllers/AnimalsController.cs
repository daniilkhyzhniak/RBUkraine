using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RBUkraine.BLL.Contracts;
using RBUkraine.BLL.Enums;
using RBUkraine.BLL.Models.Animal;
using RBUkraine.PL.ViewModels.Animals;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace RBUkraine.PL.Controllers
{
    [Route("animals")]
    public class AnimalsController : Controller
    {
        private readonly IAnimalService _animalService;
        private readonly IMapper _mapper;

        public AnimalsController(
            IAnimalService animalService,
            IMapper mapper)
        {
            _animalService = animalService;
            _mapper = mapper;
        }

        [HttpGet, Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAll(
            [FromQuery] AnimalFilterModel filter)
        {
            var animals = await _animalService.GetAllAsync(filter, CultureInfo.CurrentCulture.Name);
            var model = new AnimalsListViewModel
            {
                Animals = _mapper.Map<IList<AnimalViewModel>>(animals),
                Filter = filter
            };
            return View(model);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(
            [FromRoute] int id)
        {
            var animal = await _animalService.GetByIdAsync(id, CultureInfo.CurrentCulture.Name);
            return View(_mapper.Map<AnimalDetailsViewModel>(animal));
        }

        [HttpGet("create"), Authorize(Roles = Roles.Admin)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create"), Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Create(
            [FromForm] AnimalEditorViewModel model)
        {
            var animal = _mapper.Map<AnimalEditorModel>(model);
            var id = await _animalService.CreateAnimalAsync(animal);
            return RedirectToAction("GetById", new { id });
        }

        [HttpGet("{id:int}/edit"), Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Update(
            [FromRoute] int id)
        {
            var animal = await _animalService.GetByIdAsync(id);

            if (animal is null)
            {
                return NotFound();
            }

            return View(_mapper.Map<AnimalEditorViewModel>(animal));
        }

        [HttpPost("{id:int}/edit")]
        public async Task<IActionResult> Update(
            [FromRoute] int id,
            [FromForm] AnimalEditorViewModel model)
        {
            var animal = _mapper.Map<AnimalEditorModel>(model);
            await _animalService.UpdateAnimalAsync(id, animal);
            return RedirectToAction("GetById", new { id });
        }


        [HttpPost("{id:int}/delete")]
        public async Task<IActionResult> Delete(
            [FromRoute] int id)
        {
            await _animalService.DeleteAnimalAsync(id);
            return RedirectToAction("GetAll");
        }

        [HttpGet("{animalId:int}/translate/{culture}"), Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Translate(
            [FromRoute] int animalId, 
            [FromRoute] string culture)
        {
            if (!Culture.Exist(culture))
            {
                return BadRequest();
            }

            var animal = await _animalService.GetByIdAsync(animalId, culture);

            return View(_mapper.Map<AnimalTranslateEditorViewModel>(animal));
        }

        [HttpPost("{animalId:int}/translate/{culture}"), Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Translate(
            [FromRoute] int animalId,
            [FromRoute] string culture,
            [FromForm] AnimalTranslateEditorViewModel model)
        {
            var translate = _mapper.Map<AnimalTranslateEditorModel>(model);
            translate.Culture = culture;

            await _animalService.AddOrUpdateAnimalTranslateAsync(animalId, translate);

            return RedirectToAction("GetById", new { id = animalId });
        }
    }
}
