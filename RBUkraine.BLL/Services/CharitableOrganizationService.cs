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
using RBUkraine.DAL.Entities;
using RBUkraine.DAL.Extensions;

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

        public async Task<IEnumerable<CharitableOrganizationModel>> GetAllWithoutAnimalsAsync(string culture = Culture.Ukrainian)
        {
            var charitableOrganizations = await _context.CharitableOrganizations
                .Include(c => c.CharitableOrganizationTranslates)
                .Include(c => c.Image)
                .AsSplitQuery()
                .ToListAsync();

            return _mapper.MapToCharitableOrganizationModel(charitableOrganizations, culture);
        }

        public async Task<IEnumerable<CharitableOrganizationModel>> GetAllAsync(string culture = Culture.Ukrainian)
        {
            var charitableOrganizations = await _context.CharitableOrganizations
                .Include(c => c.CharitableOrganizationTranslates)
                .Include(c => c.Image)
                .Include(c => c.Animals)
                    .ThenInclude(a => a.AnimalTranslates)
                .Include(c => c.Animals.OrderBy(a => a.Species).Take(5))
                    .ThenInclude(a => a.AnimalImages)
                .AsSplitQuery()
                .ToListAsync();

            foreach (var animal in charitableOrganizations
                .SelectMany(charitableOrganization => charitableOrganization.Animals))
            {
                animal.CharitableOrganization = null;
            }

            return _mapper.MapToCharitableOrganizationModel(charitableOrganizations, culture);
        }

        public async Task<CharitableOrganizationModel> GetByIdAsync(int id, string culture = Culture.Ukrainian)
        {
            var charitableOrganization = await _context.CharitableOrganizations
                .Include(c => c.CharitableOrganizationTranslates)
                .Include(c => c.Image)
                .Include(c => c.Animals.Take(DefaultAnimalsCount))
                    .ThenInclude(a => a.AnimalTranslates)
                .Include(c => c.Animals.Take(DefaultAnimalsCount))
                    .ThenInclude(a => a.AnimalImages)
                .Where(c => !c.IsDeleted)
                .AsSplitQuery()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (charitableOrganization?.Animals != null)
            {
                foreach (var animal in charitableOrganization.Animals)
                {
                    animal.CharitableOrganization = null;
                }
            }

            return _mapper.MapToCharitableOrganizationModel(charitableOrganization, culture);
        }

        public async Task DeleteAsync(int id)
        {
            var charitableOrganization = await _context.CharitableOrganizations.FirstOrDefaultAsync(x => x.Id == id);

            if (charitableOrganization is null)
            {
                return;
            }

            _context.CharitableOrganizations.SoftDelete(charitableOrganization);
            await _context.SaveChangesAsync();
        }

        public async Task<int> CreateAsync(CharitableOrganizationEditorModel model)
        {
            var charitableOrganization = _mapper.Map<CharitableOrganization>(model);

            _context.CharitableOrganizations.Add(charitableOrganization);
            await _context.SaveChangesAsync();

            return charitableOrganization.Id;
        }

        public async Task UpdateAsync(int id, CharitableOrganizationEditorModel model)
        {
            var charitableOrganization = await _context.CharitableOrganizations.FirstOrDefaultAsync(x => x.Id == id);

            if (charitableOrganization is null)
            {
                return;
            }

            charitableOrganization.Name = model.Name;
            charitableOrganization.Description = model.Description;
            charitableOrganization.Email = model.Email;
            charitableOrganization.FoundationDate = model.FoundationDate;
            charitableOrganization.PhoneNumber = model.PhoneNumber;
            charitableOrganization.Stockholders = model.Stockholders;

            if (model.Image is not null)
            {
                _context.CharitableOrganizationImages.Remove(charitableOrganization.Image);
                _context.CharitableOrganizationImages.Add(new CharitableOrganizationImage
                {
                    CharitableOrganizationId = id,
                    Data = model.Image.Data,
                    Title = model.Image.Title
                });
            }
        }
    }
}
