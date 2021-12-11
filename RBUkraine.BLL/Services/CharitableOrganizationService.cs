using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RBUkraine.BLL.Contracts;
using RBUkraine.BLL.Enums;
using RBUkraine.BLL.MapperExtensions;
using RBUkraine.BLL.Models;
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

        public async Task<IEnumerable<CharitableOrganizationModel>> GetAllAdmin(CharitableOrganizationFilterModel filter, string culture = Culture.Ukrainian)
        {
            var query = _context.CharitableOrganizations
                .Include(c => c.CharitableOrganizationTranslates)
                .Include(c => c.Image)
                .Where(c => !c.IsDeleted);

            query = AddSearchFilter(query, filter);

            var charitableOrganizations = await query.AsSplitQuery().ToListAsync();
            var models = _mapper.MapToCharitableOrganizationModel(charitableOrganizations, culture);

            return Sort(models, filter);
        }

        private static IQueryable<CharitableOrganization> AddSearchFilter(IQueryable<CharitableOrganization> query, CharitableOrganizationFilterModel filter)
        {
            if (string.IsNullOrWhiteSpace(filter.Search))
            {
                return query;
            }

            var search = filter.Search.Trim().ToUpper();

            return filter.SearchOptions switch
            {
                CharitableOrganizationSearchOptions.ByName => query
                    .Where(x => x.Name.Trim().ToUpper().Contains(search)
                                || x.CharitableOrganizationTranslates.Any(a => a.Name.Trim().ToUpper().Contains(search))),
                CharitableOrganizationSearchOptions.ByDate => query.Where(x => x.FoundationDate.ToString().Contains(search)),
                _ => query
            };
        }

        private static IEnumerable<CharitableOrganizationModel> Sort(
            IEnumerable<CharitableOrganizationModel> models,
            CharitableOrganizationFilterModel filter)
        {
            return filter.SortOptions switch
            {
                CharitableOrganizationSortOptions.ByName => filter.SortDirection == SortDirection.Asc
                    ? models.OrderBy(x => x.Name)
                    : models.OrderByDescending(x => x.Name),
                CharitableOrganizationSortOptions.ByDate => filter.SortDirection == SortDirection.Asc
                    ? models.OrderBy(x => x.FoundationDate)
                    : models.OrderByDescending(x => x.FoundationDate),
                _ => filter.SortDirection == SortDirection.Asc
                    ? models.OrderBy(x => x.Name)
                    : models.OrderByDescending(x => x.Name)
            };
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
            model.Image ??= new Image
            {
                Title = "Default",
                Data = Convert.FromBase64String(Images.DefaultAnimal)
            };

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
            charitableOrganization.PhoneNumber = model.PhoneNumber;
            charitableOrganization.Founders = model.Founders;

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

            _context.Update(charitableOrganization);
            await _context.SaveChangesAsync();
        }
    }
}
