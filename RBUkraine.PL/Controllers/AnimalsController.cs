using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RBUkraine.BLL.Contracts;
using RBUkraine.BLL.Enums;
using RBUkraine.BLL.Models.Animal;
using RBUkraine.PL.ViewModels.Animals;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;

namespace RBUkraine.PL.Controllers
{
    [Route("animals")]
    public class AnimalsController : Controller
    {
        private readonly IAnimalService _animalService;
        private readonly ICharitableOrganizationService _charitableOrganizationService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public AnimalsController(
            IAnimalService animalService,
            ICharitableOrganizationService charitableOrganizationService,
            IMapper mapper,
            IStringLocalizer<SharedResource> localizer)
        {
            _animalService = animalService;
            _charitableOrganizationService = charitableOrganizationService;
            _mapper = mapper;
            _localizer = localizer;
        }

        [HttpGet("admin"), Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAll(
            [FromQuery] AnimalFilterModel filter)
        {
            var animals = await _animalService.GetAllAsync(filter, CultureInfo.CurrentCulture.Name);
            var charitableOrganizations = await _charitableOrganizationService.GetAllWithoutAnimalsAsync(CultureInfo.CurrentCulture.Name);
            var model = new AnimalsListViewModel
            {
                Animals = _mapper.Map<IList<AnimalViewModel>>(animals),
                Filter = filter,
                FoundSelectList = charitableOrganizations.Select(
                    c => new SelectListItem(c.Name, c.Id.ToString(), filter.Founds.Contains(c.Id)))
            };



            return View("GetAllAdmin", model);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(
            [FromRoute] int id)
        {
            var animal = await _animalService.GetByIdAsync(id, CultureInfo.CurrentCulture.Name);

            if (animal is null)
            {
                return NotFound();
            }

            return View(_mapper.Map<AnimalViewModel>(animal));
        }

        [HttpGet("create"), Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Create()
        {
            var charitableOrganizations = await _charitableOrganizationService
                .GetAllWithoutAnimalsAsync(CultureInfo.CurrentCulture.Name);
            var animalEditorViewModel = new AnimalEditorViewModel
            {
                CharitableOrganizations = charitableOrganizations
                    .Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList()
            }; 
            return View(animalEditorViewModel);
        }

        [HttpPost("create"), Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Create(
            [FromForm] AnimalEditorViewModel model)
        {
            var sameSpeciesAnimal = await _animalService.GetBySpecies(model.Species);

            if (sameSpeciesAnimal is not null)
            {
                ModelState.AddModelError("Species", _localizer["An animal of this species already exists"]);

                var charitableOrganizations = await _charitableOrganizationService
                    .GetAllWithoutAnimalsAsync(CultureInfo.CurrentCulture.Name);
                model.CharitableOrganizations = charitableOrganizations
                    .Select(c => new SelectListItem(c.Name, c.Id.ToString(), c.Id == model.CharitableOrganizationId)).ToList();

                return View(model);
            }

            var animal = _mapper.Map<AnimalEditorModel>(model);
            await _animalService.CreateAnimalAsync(animal);
            return RedirectToAction("GetAll");
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

            var model = _mapper.Map<AnimalEditorViewModel>(animal);
            model.Id = id;
            var charitableOrganizations = await _charitableOrganizationService
                .GetAllWithoutAnimalsAsync(CultureInfo.CurrentCulture.Name);
            model.CharitableOrganizations = charitableOrganizations
                .Select(c => new SelectListItem(c.Name, c.Id.ToString(), c.Id == model.CharitableOrganizationId)).ToList();
            return View(model);
        }

        [HttpPost("{id:int}/edit")]
        public async Task<IActionResult> Update(
            [FromRoute] int id,
            [FromForm] AnimalEditorViewModel model)
        {
            var sameSpeciesAnimal = await _animalService.GetBySpecies(model.Species);

            if (sameSpeciesAnimal is not null && sameSpeciesAnimal.Id != id)
            {
                ModelState.AddModelError("Species", _localizer["An animal of this species already exists"]);

                var charitableOrganizations = await _charitableOrganizationService
                    .GetAllWithoutAnimalsAsync(CultureInfo.CurrentCulture.Name);
                model.CharitableOrganizations = charitableOrganizations
                    .Select(c => new SelectListItem(c.Name, c.Id.ToString(), c.Id == model.CharitableOrganizationId)).ToList();

                return View(model);
            }

            var animal = _mapper.Map<AnimalEditorModel>(model);
            await _animalService.UpdateAnimalAsync(id, animal);
            return RedirectToAction("GetAll");
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
