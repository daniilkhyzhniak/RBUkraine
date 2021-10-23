using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using RBUkraine.BLL.Enums;
using RBUkraine.BLL.Models.CharitableOrganization;
using RBUkraine.DAL.Entities;
using RBUkraine.DAL.Entities.Enums;

namespace RBUkraine.BLL.MapperExtensions
{
    public static class CharitableOrganizationMapperExtensions
    {
        public static CharitableOrganizationModel MapToCharitableOrganizationModel(
            this IMapper mapper,
            CharitableOrganization charitableOrganization,
            string culture)
        {
            if (charitableOrganization is null)
            {
                return null;
            }

            var mapped = mapper.Map<CharitableOrganizationModel>(charitableOrganization);

            if (mapped.Animals?.Any() ?? false)
            {
                mapped.Animals = mapper.MapToAnimalModel(charitableOrganization.Animals, culture);
            }

            if (culture == Culture.Ukrainian)
            {
                return mapped;
            }

            var translate = charitableOrganization.CharitableOrganizationTranslates
                .FirstOrDefault(t => t.Language == Language.English);

            if (translate is not null)
            {
                mapped.Name = translate.Name;
                mapped.Description = translate.Description;
                mapped.Founders = translate.Founders;
                mapped.Stockholders = translate.Stockholders;
            }

            return mapped;
        }

        public static IEnumerable<CharitableOrganizationModel> MapToCharitableOrganizationModel(
            this IMapper mapper,
            IEnumerable<CharitableOrganization> charitableOrganizations,
            string culture)
        {
            return charitableOrganizations.Select(
                charitableOrganization => MapToCharitableOrganizationModel(mapper, charitableOrganization, culture));
        }
    }
}
