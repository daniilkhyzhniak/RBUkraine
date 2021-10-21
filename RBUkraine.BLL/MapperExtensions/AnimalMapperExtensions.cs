using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using RBUkraine.BLL.Enums;
using RBUkraine.BLL.Models.Animal;
using RBUkraine.DAL.Entities;
using RBUkraine.DAL.Entities.Enums;

namespace RBUkraine.BLL.MapperExtensions
{
    public static class AnimalMapperExtensions
    {
        public static AnimalModel MapToAnimalModel(
            this IMapper mapper,
            Animal animal,
            string culture)
        {
            if (animal is null)
            {
                return null;
            }

            var mapped = mapper.Map<AnimalModel>(animal);

            if (culture == Culture.Ukrainian)
            {
                return mapped;
            }

            var translate = animal.AnimalTranslates.FirstOrDefault(t => t.Language == Language.English);

            if (translate is not null)
            {
                mapped.Species = translate.Species;
                mapped.ConservationStatus = translate.ConservationStatus;
                mapped.Kingdom = translate.Kingdom;
                mapped.Phylum = translate.Phylum;
                mapped.Class = translate.Class;
                mapped.Order = translate.Order;
                mapped.Family = translate.Family;
                mapped.Genus = translate.Genus;
            }

            return mapped;
        }

        public static IEnumerable<AnimalModel> MapToAnimalModel(
            this IMapper mapper,
            IEnumerable<Animal> animals,
            string culture)
        {
            return animals.Select(animal => MapToAnimalModel(mapper, animal, culture));
        }

        public static AnimalDetailsModel MapToAnimalDetailsModel(
            this IMapper mapper,
            Animal animal,
            string culture)
        {
            if (animal is null)
            {
                return null;
            }

            var mapped = mapper.Map<AnimalDetailsModel>(animal);

            if (culture == Culture.Ukrainian)
            {
                return mapped;
            }

            var translate = animal.AnimalTranslates.FirstOrDefault(t => t.Language == Language.English);

            if (translate is not null)
            {
                mapped.Species = translate.Species;
                mapped.ConservationStatus = translate.ConservationStatus;
                mapped.Kingdom = translate.Kingdom;
                mapped.Phylum = translate.Phylum;
                mapped.Class = translate.Class;
                mapped.Order = translate.Order;
                mapped.Family = translate.Family;
                mapped.Genus = translate.Genus;
            }

            return mapped;
        }
    }
}
