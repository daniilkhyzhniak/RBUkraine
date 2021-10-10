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
using RBUkraine.BLL.Extensions;
using RBUkraine.DAL.Entities.Enums;

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
                translate.Name = model.Name;
                translate.Description = model.Name;
            }
            else
            {
                animal.AnimalTranslates.Add(new AnimalTranslate
                {
                    Name = model.Name,
                    Description = model.Name,
                    Language = language
                });
            }

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AnimalModel>> GetAllAsync(string culture = Culture.Ukrainian)
        {
            var animals = await _context.Animals
                .Include(animal => animal.AnimalImages)
                .Include(animal => animal.AnimalTranslates)
                .Where(animal => !animal.IsDeleted)
                .AsSplitQuery()
                .ToListAsync();


            return _mapper.MapToAnimalModel(animals, culture);
        }

        public async Task<AnimalDetailsModel> GetByIdAsync(int id, string culture = Culture.Ukrainian)
        {
            var animal = await _context.Animals
                .AsNoTracking()
                .Include(animal => animal.AnimalImages)
                .Include(animal => animal.AnimalTranslates)
                .Where(animal => !animal.IsDeleted)
                .AsSplitQuery()
                .FirstOrDefaultAsync(animal => animal.Id == id);

            return _mapper.MapToAnimalDetailsModel(animal, culture);
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
            animal.Id = id;
            _context.Animals.Update(animal);

            await _context.SaveChangesAsync();
        }
    }
}
