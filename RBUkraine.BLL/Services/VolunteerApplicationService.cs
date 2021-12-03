using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RBUkraine.BLL.Contracts;
using RBUkraine.BLL.Enums;
using RBUkraine.BLL.Models.VolunteerApplication;
using RBUkraine.DAL.Contexts;
using RBUkraine.DAL.Entities;
using RBUkraine.DAL.Extensions;

namespace RBUkraine.BLL.Services
{
    public class VolunteerApplicationService : IVolunteerApplicationService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public VolunteerApplicationService(
            AppDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task Create(VolunteerApplicationEditorModel model)
        {
            var volunteerApplication = _mapper.Map<VolunteerApplication>(model);
            _context.VolunteerApplications.Add(volunteerApplication);
            return _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<VolunteerApplicationModel>> GetAll(VolunteerApplicationFilterModel filter)
        {
            var query = _context.VolunteerApplications.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                query = query.Where(x => x.FullName.Trim().ToUpper().Contains(filter.Search.Trim().ToUpper()));
            }

            if (filter.Cities.Any())
            {
                query = query.Where(x => filter.Cities.Contains(x.City));
            }

            query = filter.SortDirection == SortDirection.Asc 
                ? query.OrderBy(x => x.City) 
                : query.OrderByDescending(x => x.City);

            var volunteerApplications = await query.ToListAsync();

            return _mapper.Map<IEnumerable<VolunteerApplicationModel>>(volunteerApplications);
        }

        public async Task ChangeStatus(int id, bool confirmed)
        {
            var volunteerApplication = await _context.VolunteerApplications.FirstOrDefaultAsync(x => x.Id == id);

            if (volunteerApplication is null)
            {
                return;
            }

            _context.VolunteerApplications.SoftDelete(volunteerApplication);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<string>> GetAllCities()
        {
            return await _context.VolunteerApplications.Select(x => x.City).ToListAsync();
        }

        public async Task<VolunteerApplicationModel> Get(int id)
        {
            var application = await _context.VolunteerApplications.FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<VolunteerApplicationModel>(application);
        }
    }
}
