using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using RBUkraine.BLL.Enums;
using RBUkraine.BLL.Models.Animal;
using RBUkraine.BLL.Models.CharitableOrganization;
using RBUkraine.BLL.Models.News;
using RBUkraine.DAL.Entities;

namespace RBUkraine.BLL.MapperExtensions
{
    public static class NewsMapperExtensions
    {
        public static NewsModel MapToNewsModel(
            this IMapper mapper,
            News news,
            string culture)
        {
            if (news is null)
            {
                return null;
            }

            var mapped = mapper.Map<NewsModel>(news);

            if (news.Animal is not null)
            {
                mapped.Animal = mapper.Map<AnimalModel>(news.Animal);
            }

            if (news.CharitableOrganization is not null)
            {
                mapped.CharitableOrganization = mapper.Map<CharitableOrganizationModel>(news.CharitableOrganization);
            }

            if (culture == Culture.Ukrainian)
            {
                return mapped;
            }

            var translate = news.NewsTranslates
                .FirstOrDefault(t => t.Language == Culture.ConvertToLanguage(culture));

            if (translate is not null)
            {
                mapped.Title = translate.Title;
                mapped.ShortDescription = translate.ShortDescription;
                mapped.FullDescription = translate.FullDescription;
            }

            return mapped;
        }

        public static IList<NewsModel> MapToNewsModel(
            this IMapper mapper,
            ICollection<News> news,
            string culture)
        {
            return news.Select(n => MapToNewsModel(mapper, n, culture)).ToList();
        }
    }
}
