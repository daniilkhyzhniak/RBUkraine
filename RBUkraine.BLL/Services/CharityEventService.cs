using System;
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
using RBUkraine.DAL.Entities;
using RBUkraine.DAL.Extensions;

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

        public async Task<IEnumerable<CharityEventModel>> GetAllAsync(
            CharityEventFilterModel filter, 
            string culture = Culture.Ukrainian)
        {
            var query = _context.CharityEvents
                .Include(c => c.CharityEventTranslates)
                .Where(c => !c.IsDeleted);

            query = AddSearchFilter(query, filter);
            query = AddSorting(query, filter);
            
            var charityEvents = await query.ToListAsync();

            return _mapper.MapToCharityEventModel(charityEvents, culture);
        }

        private static IQueryable<CharityEvent> AddSearchFilter(IQueryable<CharityEvent> query, CharityEventFilterModel filter)
        {
            if (string.IsNullOrWhiteSpace(filter.Search))
            {
                return query;
            }

            var search = filter.Search.Trim().ToUpper();

            return filter.SearchOptions switch
            {
                CharityEventSearchOptions.ByTitle => query
                    .Where(x => x.Name.Trim().ToUpper().Contains(search)
                                || x.CharityEventTranslates.Any(a => a.Name.Trim().ToUpper().Contains(search))),
                CharityEventSearchOptions.ByOrganizer => query
                    .Where(x => x.Organizer.Trim().ToUpper().Contains(search)
                                || x.CharityEventTranslates.Any(a => a.Organizer.Trim().ToUpper().Contains(search))),
                CharityEventSearchOptions.ByDate => query.Where(x => x.DateTime.ToString().Contains(search)),
                CharityEventSearchOptions.ByPrice => query.Where(x => x.Price.ToString().Contains(search)),
                _ => query
            };
        }

        private static IQueryable<CharityEvent> AddSorting(IQueryable<CharityEvent> query, CharityEventFilterModel filter)
        {
            return filter.SortOptions switch
            {
                CharityEventSortOptions.ByTitle => filter.SortDirection == SortDirection.Asc
                    ? query.OrderBy(x => x.Name)
                    : query.OrderByDescending(x => x.Name),
                CharityEventSortOptions.ByPrice => filter.SortDirection == SortDirection.Asc
                    ? query.OrderBy(x => x.Name)
                    : query.OrderByDescending(x => x.Name),
                _ => filter.SortDirection == SortDirection.Asc
                    ? query.OrderBy(x => x.DateTime)
                    : query.OrderByDescending(x => x.DateTime),
            };
        }

        public async Task<CharityEventModel> GetByIdAsync(int id, string culture = Culture.Ukrainian)
        {
            var charityEvent = await _context.CharityEvents
                .Include(c => c.CharityEventTranslates)
                .Where(c => !c.IsDeleted)
                .FirstOrDefaultAsync(c => c.Id == id);

            return _mapper.MapToCharityEventModel(charityEvent, culture);
        }

        public async Task DeleteAsync(int id)
        {
            var charityEvent = await _context.CharityEvents.FirstOrDefaultAsync(x => x.Id == id);

            if (charityEvent is null)
            {
                return;
            }

            _context.CharityEvents.SoftDelete(charityEvent);
            await _context.SaveChangesAsync();
        }

        public async Task<int> CreateAsync(CharityEventEditorModel model)
        {
            var charityEvent = _mapper.Map<CharityEvent>(model);

            _context.CharityEvents.Add(charityEvent);
            await _context.SaveChangesAsync();

            return charityEvent.Id;
        }

        public async Task UpdateAsync(int id, CharityEventEditorModel model)
        {
            var charityEvent = await _context.CharityEvents.FirstOrDefaultAsync(x => x.Id == id);

            if (charityEvent is null)
            {
                return;
            }

            charityEvent.Name = model.Name;
            charityEvent.Description = model.Description;
            charityEvent.Organizer = model.Organizer;
            charityEvent.DateTime = model.DateTime;
            charityEvent.Price = model.Price;

            _context.CharityEvents.Update(charityEvent);
            await _context.SaveChangesAsync();
        }

        public async Task AddPurchase(int eventId, int userId)
        {
            var charityEvent = await _context.CharityEvents.FirstOrDefaultAsync(x => x.Id == eventId);

            _context.CharityEventPurchases.Add(new CharityEventPurchase
            {
                CharityEventId = eventId,
                UserId = userId,
                Price = charityEvent.Price,
                PurchaseDate = DateTimeOffset.Now
            });
            await _context.SaveChangesAsync();
        }
    }
}
