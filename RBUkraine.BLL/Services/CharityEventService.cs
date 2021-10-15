using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RBUkraine.BLL.Contracts;
using RBUkraine.BLL.Enums;
using RBUkraine.BLL.MapperExtensions;
using RBUkraine.BLL.Models.CharityEvent;
using RBUkraine.DAL.Contexts;

namespace RBUkraine.BLL.Services
{
    public class CharityEventService : ICharityEventService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CharityEventService(
            AppDbContext context, 
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CharityEventModel>> GetAllAsync(string culture = Culture.Ukrainian)
        {
            var charityEvents = await _context.CharityEvents
                .Include(c => c.CharityEventTranslates)
                .Where(c => !c.IsDeleted)
                .ToListAsync();

            return _mapper.MapToCharityEventModel(charityEvents, culture);
        }

        public async Task<CharityEventModel> GetByIdAsync(int id, string culture = Culture.Ukrainian)
        {
            var charityEvent = await _context.CharityEvents
                .Include(c => c.CharityEventTranslates)
                .Where(c => !c.IsDeleted)
                .FirstOrDefaultAsync(c => c.Id == id);

            return _mapper.MapToCharityEventModel(charityEvent, culture);
        }
    }
}
