using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RBUkraine.BLL.Contracts;
using RBUkraine.BLL.Enums;
using RBUkraine.BLL.Models.CharitableOrganization;
using RBUkraine.PL.ViewModels.CharitableOrganizations;

namespace RBUkraine.PL.Controllers
{
    [Route("organizations")]
    public class CharitableOrganizationsController : Controller
    {
        private readonly ICharitableOrganizationService _charitableOrganizationService;
        private readonly IMapper _mapper;

        public CharitableOrganizationsController(
            ICharitableOrganizationService charitableOrganizationService,
            IMapper mapper)
        {
            _charitableOrganizationService = charitableOrganizationService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("~/")]
        [Route("~/animals")]
        public async Task<IActionResult> GetAnimals()
        {
            var charitableOrganizations = await _charitableOrganizationService.GetAllAsync(CultureInfo.CurrentCulture.Name);
            return View("GetAllAnimals", _mapper.Map<IEnumerable<CharitableOrganizationViewModel>>(charitableOrganizations));
        }

        [HttpGet]
        public async Task<IActionResult> GetOrganizations()
        {
            var charitableOrganizations = await _charitableOrganizationService.GetAllAsync(CultureInfo.CurrentCulture.Name);
            return View("GetAll", _mapper.Map<IEnumerable<CharitableOrganizationViewModel>>(charitableOrganizations));
        }

        [HttpGet("{id:int}/animals")]
        public async Task<IActionResult> GetById(int id)
        {
            var charitableOrganization = await _charitableOrganizationService.GetByIdAsync(id, CultureInfo.CurrentCulture.Name);

            if (charitableOrganization is null)
            {
                return NotFound();
            }

            return View("GetById", _mapper.Map<CharitableOrganizationViewModel>(charitableOrganization));
        }

        [HttpGet("admin"), Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllAdmin()
        {
            var charitableOrganizations = await _charitableOrganizationService.GetAllAdmin(CultureInfo.CurrentCulture.Name);
            return View(_mapper.Map<IEnumerable<CharitableOrganizationViewModel>>(charitableOrganizations));
        }

        [HttpGet("create"), Authorize(Roles = Roles.Admin)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create"), Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Create(CharitableOrganizationEditorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _charitableOrganizationService.CreateAsync(_mapper.Map<CharitableOrganizationEditorModel>(model));
            return RedirectToAction();
        }

        [HttpGet("{id:int}/edit"), Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Update(int id)
        {
            var charitableOrganization =
                await _charitableOrganizationService.GetByIdAsync(id, CultureInfo.CurrentCulture.Name);

            if (charitableOrganization is null)
            {
                return NotFound("GetAllAdmin");
            }

            return View(_mapper.Map<CharitableOrganizationEditorViewModel>(charitableOrganization));
        }

        [HttpPost("{id:int}/edit"), Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Update(int id, CharitableOrganizationEditorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _charitableOrganizationService.UpdateAsync(id, _mapper.Map<CharitableOrganizationEditorModel>(model));
            return RedirectToAction("GetAllAdmin");
        }

        [HttpPost("{id:int}/delete")]
        public async Task<IActionResult> Delete(
            [FromRoute] int id)
        {
            await _charitableOrganizationService.DeleteAsync(id);
            return RedirectToAction("GetAllAdmin");
        }
    }
}
