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

        public async Task<IEnumerable<AnimalModel>> GetAllAsync()
        {
            var animals = await _context.Animals
                .Include(animal => animal.AnimalImages)
                .Where(animal => !animal.IsDeleted)
                .ToListAsync();
            return _mapper.Map<IEnumerable<AnimalModel>>(animals);
        }

        public async Task<AnimalDetailsModel> GetByIdAsync(int id)
        {
            var animal = await _context.Animals
                .AsNoTracking()
                .Include(animal => animal.AnimalImages)
                .Where(animal => !animal.IsDeleted)
                .FirstOrDefaultAsync(animal => animal.Id == id);

            return _mapper.Map<AnimalDetailsModel>(animal);
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
