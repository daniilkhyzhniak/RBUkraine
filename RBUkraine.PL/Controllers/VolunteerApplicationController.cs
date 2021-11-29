using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RBUkraine.BLL.Contracts;
using RBUkraine.BLL.Enums;
using RBUkraine.BLL.Models.VolunteerApplication;
using RBUkraine.PL.EmailSender;
using RBUkraine.PL.EmailSender.Models;
using RBUkraine.PL.ViewModels.VolunteerApplication;

namespace RBUkraine.PL.Controllers
{
    [Route("volunteers")]
    public class VolunteerApplicationController : Controller
    {
        private readonly IVolunteerApplicationService _volunteerApplicationService;
        private readonly IEmailSender emailSender;

        public VolunteerApplicationController(
            IVolunteerApplicationService volunteerApplicationService,
            IEmailSender emailSender)
        {
            _volunteerApplicationService = volunteerApplicationService;
            this.emailSender = emailSender;
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

        [HttpPost("{id}/confirm"), Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Confirm([FromRoute] int id)
        {
            var application = await _volunteerApplicationService.Get(id);

            if (application is null)
            {
                return BadRequest();
            }

            await Task.WhenAll(
                _volunteerApplicationService.ChangeStatus(id, true),
                emailSender.SendEmailAsync(new EmailModel
                {
                    Email = application.Email,
                    Subject = "Ваша заявка на волонтерство была принята, ждем вас в нашем офисе в течении 7 дней для введения в курс дела",
                    Message = "Ваша заявка на волонтерство была принята, ждем вас в нашем офисе в течении 7 дней для введения в курс дела"
                }));

            return Ok();
        }

        [HttpPost("{id}/decline"), Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Decline([FromRoute] int id)
        {
            var application = await _volunteerApplicationService.Get(id);

            if (application is null)
            {
                return BadRequest();
            }

            await Task.WhenAll(
                _volunteerApplicationService.ChangeStatus(id, false),
                emailSender.SendEmailAsync(new EmailModel
                {
                    Email = application.Email,
                    Subject = "Ваша заявка на волонтерство была отклонена",
                    Message = "Ваша заявка на волонтерство была отклонена"
                }));

            return Ok();
        }
    }
}
