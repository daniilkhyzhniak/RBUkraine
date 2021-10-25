using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RBUkraine.BLL.Contracts;
using RBUkraine.BLL.Models.Animal;
using RBUkraine.DAL.Contexts;
using RBUkraine.DAL.Entities;
using RBUkraine.DAL.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RBUkraine.BLL.Enums;
using RBUkraine.BLL.MapperExtensions;

namespace RBUkraine.BLL.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AnimalService(
            AppDbContext context, 
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreateAnimalAsync(AnimalEditorModel model)
        {
            if (!model.Images.Any())
            {
                throw new ArgumentException("Animal should have at least one picture");
            }

            var animal = _mapper.Map<Animal>(model);

            _context.Animals.Add(animal);
            await _context.SaveChangesAsync();

            return animal.Id;
        }

        public async Task DeleteAnimalAsync(int id)
        {
            var animal = await _context.Animals.FirstOrDefaultAsync(animal => animal.Id == id);

            if (animal is null)
            {
                return;
            }

            _context.Animals.SoftDelete(animal);
            await _context.SaveChangesAsync();
        }
        
        public async Task AddOrUpdateAnimalTranslateAsync(int animalId, AnimalTranslateEditorModel model)
        {
            var animal = await _context.Animals
                .Include(animal => animal.AnimalTranslates)
                .Where(animal => !animal.IsDeleted)
                .FirstOrDefaultAsync(animal => animal.Id == animalId);

            if (animal is null)
            {
                throw new ArgumentException("Animal does not exist");
            }

            var language = Culture.ConvertToLanguage(model.Culture);
            var translate = animal.AnimalTranslates
                .FirstOrDefault(t => t.Language == language);

            if (translate is not null)
            {
                translate.Species = model.Species;
                translate.ConservationStatus = model.ConservationStatus;
                translate.Kingdom = model.Kingdom;
                translate.Phylum = model.Phylum;
                translate.Class = model.Class;
                translate.Order = model.Order;
                translate.Family = model.Family;
                translate.Genus = model.Genus;
            }
            else
            {
                animal.AnimalTranslates.Add(new AnimalTranslate
                {
                    Species = model.Species,
                    ConservationStatus = model.ConservationStatus,
                    Kingdom = model.Kingdom,
                    Phylum = model.Phylum,
                    Class = model.Class,
                    Order = model.Order,
                    Family = model.Family,
                    Genus = model.Genus,
                    Language = language
                });
            }

            await _context.SaveChangesAsync();
        }
        
        public async Task<IEnumerable<AnimalModel>> GetAllAsync(AnimalFilterModel filter, string culture = Culture.Ukrainian)
        {
            var query = _context.Animals
                .Include(x => x.AnimalImages)
                .Include(animal => animal.AnimalTranslates)
                .Include(x => x.CharitableOrganization)
                    .ThenInclude(x => x.CharitableOrganizationTranslates)
                .Include(x => x.CharitableOrganization)
                    .ThenInclude(x => x.Image)
                .Where(animal => !animal.IsDeleted);

            query = AddSearchFilter(query, filter);
            query = AddSorting(query, filter);

            if (filter.Founds.Any())
            {
                query = query.Where(a => filter.Founds.Contains(a.CharitableOrganizationId));
            }

            var animals = await query
                .ToListAsync();

            foreach (var animal in animals)
            {
                animal.CharitableOrganization.Animals = null;
            }

            return _mapper.MapToAnimalModel(animals, culture);
        }

        private static IQueryable<Animal> AddSearchFilter(IQueryable<Animal> query, AnimalFilterModel filter)
        {
            if (string.IsNullOrWhiteSpace(filter.Search))
            {
                return query;
            }

            var search = filter.Search.Trim().ToUpper();

            return filter.SearchOptions switch
            {
                AnimalsSearchOptions.BySpecious => query
                    .Where(x => x.Species.Trim().ToUpper().Contains(search)
                                || x.AnimalTranslates.Any(a => a.Species.Trim().ToUpper().Contains(search))),
                AnimalsSearchOptions.ByLatinSpecious => query
                    .Where(x => x.LatinSpecies.Trim().ToUpper().Contains(search)),
                AnimalsSearchOptions.ByCharitableOrganization => query
                    .Where(x => x.CharitableOrganization.Name.Trim().ToUpper().Contains(search)
                                || x.CharitableOrganization.CharitableOrganizationTranslates
                                    .Any(a => a.Name.Trim().ToUpper().Contains(search))),
                _ => query
            };
        }

        private static IQueryable<Animal> AddSorting(IQueryable<Animal> query, AnimalFilterModel filter)
        {
            return filter.SortOptions switch
            {
                AnimalsSortOptions.ByLatinSpecies => filter.SortDirection == SortDirection.Asc
                    ? query.OrderBy(x => x.Species)
                    : query.OrderByDescending(x => x.Species),
                AnimalsSortOptions.ByCharitableOrganization => filter.SortDirection == SortDirection.Asc
                    ? query.OrderBy(x => x.Species)
                    : query.OrderByDescending(x => x.Species),
                _ => filter.SortDirection == SortDirection.Asc
                    ? query.OrderBy(x => x.Species)
                    : query.OrderByDescending(x => x.Species),
            };
        }

        public async Task<AnimalModel> GetByIdAsync(int id, string culture = Culture.Ukrainian)
        {
            var animal = await _context.Animals
                .AsNoTracking()
                .Include(animal => animal.AnimalImages)
                .Include(animal => animal.AnimalTranslates)
                .Include(x => x.CharitableOrganization)
                    .ThenInclude(x => x.CharitableOrganizationTranslates)
                .Include(x => x.CharitableOrganization)
                    .ThenInclude(x => x.Image)
                .Where(animal => !animal.IsDeleted)
                .AsSplitQuery()
                .FirstOrDefaultAsync(animal => animal.Id == id);

            if (animal is not null)
            {
                animal.CharitableOrganization.Animals = null;
            }

            return _mapper.MapToAnimalModel(animal, culture);
        }

        public async Task UpdateAnimalAsync(int id, AnimalEditorModel model)
        {
            if (!model.Images.Any())
            {
                return;
            }

            var animal = await _context.Animals
                .AsNoTracking()
                .Include(animal => animal.AnimalImages)
                .FirstOrDefaultAsync(animal => animal.Id == id);

            if (animal is null)
            {
                return;
            }

            _context.AnimalImages.RemoveRange(animal.AnimalImages);

            animal = _mapper.Map<Animal>(model);
            animal.Species = model.Species;
            animal.ConservationStatus = model.ConservationStatus;
            animal.Kingdom = model.Kingdom;
            animal.Phylum = model.Phylum;
            animal.Class = model.Class;
            animal.Order = model.Order;
            animal.Family = model.Family;
            animal.Genus = model.Genus;
            animal.Description = model.Description;
            animal.Population = animal.Population;
            _context.Animals.Update(animal);

            await _context.SaveChangesAsync();
        }
    }
}
