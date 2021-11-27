using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RBUkraine.BLL.Contracts;
using RBUkraine.BLL.Enums;
using RBUkraine.BLL.Models.VolunteerApplication;
using RBUkraine.PL.ViewModels.VolunteerApplication;

namespace RBUkraine.PL.Controllers
{
    [Route("volunteers")]
    public class VolunteerApplicationController : Controller
    {
        private readonly IVolunteerApplicationService _volunteerApplicationService;

        public VolunteerApplicationController(
            IVolunteerApplicationService volunteerApplicationService)
        {
            _volunteerApplicationService = volunteerApplicationService;
        }

        [HttpGet("admin"), Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllAdmin(VolunteerApplicationFilterModel filter)
        {
            var cities = await _volunteerApplicationService.GetAllCities();
            return View(new VolunteerApplicationListViewModel
            {
                Filter = filter,
                VolunteerApplications = await _volunteerApplicationService.GetAll(filter),
                CitiesSelectList = cities.Select(x => new SelectListItem(x, x, filter.Cities.Contains(x)))
            });
        }

        [HttpGet("apply")]
        public IActionResult Apply()
        {
            return View();
        }

        [HttpPost("apply")]
        public async Task<IActionResult> Apply(VolunteerApplicationEditorModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _volunteerApplicationService.Create(model);
            return RedirectToAction("GetAnimals", "CharitableOrganizations");
        }

        [HttpPost("confirm"), Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Confirm([FromRoute] int id)
        {
            await _volunteerApplicationService.ChangeStatus(id, true);
            return Ok();
        }

        [HttpPost("decline"), Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Decline([FromRoute] int id)
        {
            await _volunteerApplicationService.ChangeStatus(id, false);
            return Ok();
        }
    }
}
