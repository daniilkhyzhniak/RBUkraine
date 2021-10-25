using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RBUkraine.BLL.Contracts;
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
    }
}
