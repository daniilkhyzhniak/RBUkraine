using System;
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
using RBUkraine.BLL.Models.News;
using RBUkraine.PL.ViewModels.News;

namespace RBUkraine.PL.Controllers
{
    [Route("news")]
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        private readonly IAnimalService _animalService;
        private readonly ICharitableOrganizationService _charitableOrganizationService;
        private readonly IMapper _mapper;

        public NewsController(
            INewsService newsService,
            IAnimalService animalService,
            ICharitableOrganizationService charitableOrganizationService,
            IMapper mapper)
        {
            _newsService = newsService;
            _animalService = animalService;
            _charitableOrganizationService = charitableOrganizationService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(NewsFilterModel filter)
        {
            var news = await _newsService.GetAllAsync(filter, CultureInfo.CurrentCulture.Name);
            return View(_mapper.Map<List<NewsViewModel>>(news));
        }

        [HttpGet("admin"), Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllAdmin(NewsFilterModel filter)
        {
            var news = await _newsService.GetAllAsync(filter, CultureInfo.CurrentCulture.Name);
            var charitableOrganizations = await _charitableOrganizationService
                .GetAllWithoutAnimalsAsync(CultureInfo.CurrentCulture.Name);

            var model = new NewsListViewModel
            {
                News = _mapper.Map<List<NewsViewModel>>(news),
                Filter = filter,
                FoundSelectList = charitableOrganizations.Select(
                    c => new SelectListItem(c.Name, c.Id.ToString(), filter.Founds.Contains(c.Id)))
            };
            return View(model);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var news = await _newsService.GetByIdAsync(id, CultureInfo.CurrentCulture.Name);
            return View(_mapper.Map<NewsViewModel>(news));
        }

        [HttpGet("create"), Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Create()
        {
            var animals = await _animalService.GetAllAsync(CultureInfo.CurrentCulture.Name);
            var charitableOrganizations =
                await _charitableOrganizationService.GetAllAsync(CultureInfo.CurrentCulture.Name);

            var model = new NewsEditorViewModel
            {
                AnimalSelectList = animals
                    .Select(x => new SelectListItem(x.Species, x.Id.ToString())).ToList(),
                CharitableOrganizationSelectList = charitableOrganizations
                    .Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList()
            };

            return View(model);
        }

        [HttpPost("create"), Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Create(
            [FromForm] NewsEditorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var animals = await _animalService.GetAllAsync(CultureInfo.CurrentCulture.Name);
                var charitableOrganizations =
                    await _charitableOrganizationService.GetAllAsync(CultureInfo.CurrentCulture.Name);
                model.AnimalSelectList = animals
                    .Select(x => new SelectListItem(x.Species, x.Id.ToString())).ToList();
                model.CharitableOrganizationSelectList = charitableOrganizations
                    .Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();

                return View(model);
            }

            var news = _mapper.Map<NewsEditorModel>(model);
            await _newsService.CreateAsync(news);

            return RedirectToAction("GetAllAdmin");
        }

        [HttpGet("{id}/edit"), Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Update(int id)
        {
            var news = await _newsService.GetByIdAsync(id, CultureInfo.CurrentCulture.Name);

            if (news is null)
            {
                return NotFound();
            }

            var animals = await _animalService.GetAllAsync(CultureInfo.CurrentCulture.Name);
            var charitableOrganizations =
                await _charitableOrganizationService.GetAllAsync(CultureInfo.CurrentCulture.Name);

            var model = new NewsEditorViewModel
            {
                AnimalSelectList = animals
                    .Select(x => new SelectListItem(x.Species, x.Id.ToString())).ToList(),
                CharitableOrganizationSelectList = charitableOrganizations
                    .Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList()
            };

            return View(model);
        }

        [HttpPost("{id}/edit"), Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Update(
            int id,
            [FromForm] NewsEditorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var animals = await _animalService.GetAllAsync(CultureInfo.CurrentCulture.Name);
                var charitableOrganizations =
                    await _charitableOrganizationService.GetAllAsync(CultureInfo.CurrentCulture.Name);
                model.AnimalSelectList = animals
                    .Select(x => new SelectListItem(x.Species, x.Id.ToString())).ToList();
                model.CharitableOrganizationSelectList = charitableOrganizations
                    .Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();

                return View(model);
            }

            var news = _mapper.Map<NewsEditorModel>(model);
            await _newsService.UpdateAsync(id, news);

            return RedirectToAction("GetAllAdmin");
        }

        [HttpPost("{id}/delete"), Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            await _newsService.DeleteAsync(id);
            return RedirectToAction("GetAllAdmin");
        }
    }
}
