using System.Collections.Generic;
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
            var news = await _context.News
                .Include(x => x.NewsTranslates)
                .Include(x => x.Animal)
                .Include(x => x.CharitableOrganization)
                .ToListAsync();

            return _mapper.MapToNewsModel(news, culture);
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
