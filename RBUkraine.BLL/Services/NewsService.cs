using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RBUkraine.BLL.Contracts;
using RBUkraine.BLL.Enums;
using RBUkraine.BLL.MapperExtensions;
using RBUkraine.BLL.Models.News;
using RBUkraine.DAL.Contexts;
using RBUkraine.DAL.Entities;
using RBUkraine.DAL.Extensions;

namespace RBUkraine.BLL.Services
{
    public class NewsService : INewsService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public NewsService(
            AppDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NewsModel>> GetAllAsync(string culture = Culture.Ukrainian)
        {
            var news = await _context.News
                .Include(x => x.NewsTranslates)
                .Include(x => x.Animal)
                .Include(x => x.CharitableOrganization)
                .ToListAsync();

            return _mapper.MapToNewsModel(news, culture);
        }

        public async Task<IEnumerable<NewsModel>> GetAllAsync(NewsFilterModel filter, string culture = Culture.Ukrainian)
        {
            IQueryable<News> query = _context.News
                .Include(x => x.NewsTranslates)
                .Include(x => x.Animal)
                .Include(x => x.CharitableOrganization);

            if (filter.Founds.Any())
            {
                query = query.Where(a => a.CharitableOrganizationId.HasValue && filter.Founds.Contains(a.CharitableOrganizationId.Value));
            }

            query = AddSearchFilter(query, filter);
            query = AddSorting(query, filter);

            var news = await query.ToListAsync();

            return _mapper.MapToNewsModel(news, culture);
        }

        private static IQueryable<News> AddSearchFilter(IQueryable<News> query, NewsFilterModel filter)
        {
            if (string.IsNullOrWhiteSpace(filter.Search))
            {
                return query;
            }

            var search = filter.Search.Trim().ToUpper();

            return filter.SearchOptions switch
            {
                NewsSearchOptions.ByTitle => query
                    .Where(x => x.Title.Trim().ToUpper().Contains(search) || x.NewsTranslates.Any(a => a.Title.Trim().ToUpper().Contains(search))),
                NewsSearchOptions.ByCharitableOrganization => query.Where(x => x.CharitableOrganizationId != null 
                                                                               && x.CharitableOrganization.Name.Trim().ToUpper().Contains(search)
                                                                               || x.CharitableOrganization.CharitableOrganizationTranslates
                                                                                   .Any(a => a.Name.Trim().ToUpper().Contains(search))),
                NewsSearchOptions.ByDate => query.Where(x => x.PublishDate.ToString().Contains(search)),
                NewsSearchOptions.BySpecies => query.Where(x => x.AnimalId != null 
                                                                && x.Animal.Species.ToString().Contains(search)
                                                                || x.Animal.AnimalTranslates.Any(a => a.Species.Trim().ToUpper().Contains(search))),
                _ => query
            };
        }

        private static IQueryable<News> AddSorting(IQueryable<News> query, NewsFilterModel filter)
        {
            return filter.SortOptions switch
            {
                NewsSortOptions.BySpecies => filter.SortDirection == SortDirection.Asc
                    ? query.OrderBy(x => x.Animal.Species)
                    : query.OrderByDescending(x => x.Animal.Species),
                NewsSortOptions.ByCharitableOrganization => filter.SortDirection == SortDirection.Asc
                    ? query.OrderBy(x => x.CharitableOrganization.Name)
                    : query.OrderByDescending(x => x.CharitableOrganization.Name),
                NewsSortOptions.ByDate => filter.SortDirection == SortDirection.Asc
                    ? query.OrderBy(x => x.PublishDate)
                    : query.OrderByDescending(x => x.PublishDate),
                _ => filter.SortDirection == SortDirection.Asc
                    ? query.OrderBy(x => x.Title)
                    : query.OrderByDescending(x => x.Title)
            };
        }

        public async Task<NewsModel> GetByIdAsync(int id, string culture = Culture.Ukrainian)
        {
            var news = await _context.News
                .Include(x => x.NewsTranslates)
                .Include(x => x.Animal)
                .Include(x => x.CharitableOrganization)
                .FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.MapToNewsModel(news, culture);
        }

        public async Task DeleteAsync(int id)
        {
            var news = await _context.News.FirstOrDefaultAsync(x => x.Id == id);

            if (news is null)
            {
                return;
            }

            _context.News.SoftDelete(news);
            await _context.SaveChangesAsync();
        }

        public async Task<int> CreateAsync(NewsEditorModel model)
        {
            var news = _mapper.Map<News>(model);

            _context.News.Add(news);
            await _context.SaveChangesAsync();

            return news.Id;
        }

        public async Task UpdateAsync(int id, NewsEditorModel model)
        {
            var news = await _context.News.FirstOrDefaultAsync(x => x.Id == id);

            if (news is null)
            {
                return;
            }

            news.Title = model.Title;
            news.ShortDescription = model.ShortDescription;
            news.FullDescription = model.FullDescription;
            news.AnimalId = model.AnimalId;
            news.CharitableOrganizationId = model.CharitableOrganizationId;
            news.PublishDate = model.PublishDate;

            _context.News.Update(news);
            await _context.SaveChangesAsync();
        }
    }
}
