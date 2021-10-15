using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using RBUkraine.BLL.Enums;
using RBUkraine.BLL.Models.CharityEvent;
using RBUkraine.DAL.Entities;
using RBUkraine.DAL.Entities.Enums;

namespace RBUkraine.BLL.MapperExtensions
{
    public static class CharityEventMapperExtensions
    {
        public static CharityEventModel MapToCharityEventModel(
            this IMapper mapper,
            CharityEvent charityEvent,
            string culture)
        {
            var mapped = mapper.Map<CharityEventModel>(charityEvent);

            if (culture == Culture.Ukrainian)
            {
                return mapped;
            }

            var translate = charityEvent.CharityEventTranslates.FirstOrDefault(t => t.Language == Language.English);

            if (translate is not null)
            {
                mapped.Name = translate.Name;
            }

            return mapped;
        }

        public static IEnumerable<CharityEventModel> MapToCharityEventModel(
            this IMapper mapper,
            IEnumerable<CharityEvent> charityEvents,
            string culture)
        {
            return charityEvents.Select(animal => MapToCharityEventModel(mapper, animal, culture));
        }
    }
}
