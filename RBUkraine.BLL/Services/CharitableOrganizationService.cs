using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RBUkraine.BLL.Contracts;
using RBUkraine.BLL.Enums;
using RBUkraine.BLL.MapperExtensions;
using RBUkraine.BLL.Models.CharitableOrganization;
using RBUkraine.DAL.Contexts;

namespace RBUkraine.BLL.Services
{
    public class CharitableOrganizationService : ICharitableOrganizationService
    {
        private const int DefaultAnimalsCount = 5;

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CharitableOrganizationService(
            AppDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CharitableOrganizationModel>> GetAllAsync(string culture = Culture.Ukrainian)
        {
            var charitableOrganizations = await _context.CharitableOrganizations
                .Include(c => c.CharitableOrganizationTranslates)
                .Include(c => c.Animals.Take(DefaultAnimalsCount))
                    .ThenInclude(a => a.AnimalTranslates)
                .Include(c => c.Animals.Take(DefaultAnimalsCount))
                    .ThenInclude(a => a.AnimalImages)
                .Where(c => !c.IsDeleted)
                .AsSplitQuery()
                .ToListAsync();

            return _mapper.MapToCharitableOrganizationModel(charitableOrganizations, culture);
        }

        public async Task<CharitableOrganizationModel> GetByIdAsync(int id, string culture = Culture.Ukrainian)
        {
            var charitableOrganization = await _context.CharitableOrganizations
                .Include(c => c.CharitableOrganizationTranslates)
                .Include(c => c.Animals.Take(DefaultAnimalsCount))
                    .ThenInclude(a => a.AnimalTranslates)
                .Include(c => c.Animals.Take(DefaultAnimalsCount))
                    .ThenInclude(a => a.AnimalImages)
                .Where(c => !c.IsDeleted)
                .AsSplitQuery()
                .FirstOrDefaultAsync(c => c.Id == id);

            return _mapper.MapToCharitableOrganizationModel(charitableOrganization, culture);
        }
    }
}
