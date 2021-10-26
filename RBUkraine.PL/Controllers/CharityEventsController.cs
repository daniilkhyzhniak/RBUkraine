using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RBUkraine.BLL.Contracts;
using RBUkraine.PL.ViewModels.CharityEvents;

namespace RBUkraine.PL.Controllers
{
    [Route("events")]
    public class CharityEventsController : Controller
    {
        private readonly ICharityEventService _charityEventService;
        private readonly IMapper _mapper;

        public CharityEventsController(
            ICharityEventService charityEventService,
            IMapper mapper)
        {
            _charityEventService = charityEventService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var charityEvents = await _charityEventService.GetAllAsync(CultureInfo.CurrentCulture.Name);
            return View(_mapper.Map<IEnumerable<CharityEventViewModel>>(charityEvents));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var charityEvent = await _charityEventService.GetByIdAsync(id, CultureInfo.CurrentCulture.Name);

            if (charityEvent is null)
            {
                return NotFound();
            }

            return View(_mapper.Map<CharityEventViewModel>(charityEvent));
        }
    }
}
