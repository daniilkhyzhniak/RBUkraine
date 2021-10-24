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
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            var charitableOrganizations = await _charitableOrganizationService.GetAllAsync(CultureInfo.CurrentCulture.Name);
            return View(_mapper.Map<IEnumerable<CharitableOrganizationViewModel>>(charitableOrganizations));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var charitableOrganization = await _charitableOrganizationService.GetByIdAsync(id, CultureInfo.CurrentCulture.Name);

            if (charitableOrganization is null)
            {
                return NotFound();
            }

            return View(_mapper.Map<CharitableOrganizationViewModel>(charitableOrganization));
        }
    }
}
