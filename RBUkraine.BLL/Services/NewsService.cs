using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using RBUkraine.BLL.Contracts;
using RBUkraine.BLL.Enums;
using RBUkraine.BLL.Models.News;
using RBUkraine.DAL.Contexts;

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

        public Task<IEnumerable<NewsModel>> GetAllAsync(string culture = Culture.Ukrainian)
        {
            throw new NotImplementedException();
        }

        public Task<NewsModel> GetByIdAsync(int id, string culture = Culture.Ukrainian)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> CreateAsync(NewsEditorModel model)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, NewsEditorModel model)
        {
            throw new NotImplementedException();
        }
    }
}
