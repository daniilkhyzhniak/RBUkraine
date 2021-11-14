using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RBUkraine.BLL.Contracts;
using RBUkraine.BLL.Enums;
using RBUkraine.BLL.Models;
using RBUkraine.BLL.Models.Product;
using RBUkraine.DAL.Contexts;
using RBUkraine.DAL.Entities;
using RBUkraine.DAL.Extensions;

namespace RBUkraine.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(
            AppDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<IEnumerable<ProductModel>> GetAll(string culture = Culture.Ukrainian)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductModel>> GetAll(ProductFilterModel filter, string culture = Culture.Ukrainian)
        {
            var query = _context.Products
                .Include(x => x.Image)
                .Where(x => !x.IsDeleted);

            query = AddSearchFilter(query, filter);

            if (filter.Categories.Any())
            {
                query = query.Where(a => filter.Categories.Contains(a.Category));
            }

            var products = await query.AsSplitQuery().ToListAsync();
            var models = _mapper.Map<IEnumerable<ProductModel>>(products);

            return Sort(models, filter);
        }

        public async Task<IEnumerable<ProductModel>> GetAll(IEnumerable<int> ids)
        {
            var products = await _context.Products
                .Include(x => x.Image)
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();

            return _mapper.Map<IEnumerable<ProductModel>>(products);
        }

        private static IQueryable<Product> AddSearchFilter(IQueryable<Product> query, ProductFilterModel filter)
        {
            if (string.IsNullOrWhiteSpace(filter.Search))
            {
                return query;
            }

            var search = filter.Search.Trim().ToUpper();

            return query.Where(x => x.Name.Trim().ToUpper().Contains(search));
        }

        private static IEnumerable<ProductModel> Sort(
            IEnumerable<ProductModel> models,
            ProductFilterModel filter)
        {
            return filter.SortOptions switch
            {
                ProductSortOptions.ByCategory => filter.SortDirection == SortDirection.Asc
                    ? models.OrderBy(x => x.Category)
                    : models.OrderByDescending(x => x.Category),
                ProductSortOptions.ByPrice => filter.SortDirection == SortDirection.Asc
                    ? models.OrderBy(x => x.Price)
                    : models.OrderByDescending(x => x.Price),
                _ => filter.SortDirection == SortDirection.Asc
                    ? models.OrderBy(x => x.Name)
                    : models.OrderByDescending(x => x.Name)
            };
        }

        public async Task<ProductModel> Get(int id, string culture = Culture.Ukrainian)
        {
            var product = await _context.Products
                .Include(x => x.Image)
                .FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<ProductModel>(product);
        }

        public Task Create(ProductEditorModel model)
        {
            model.Image ??= new Image
            {
                Title = "Default",
                Data = Convert.FromBase64String(Images.DefaultAnimal)
            };

            _context.Products.Add(_mapper.Map<Product>(model));
            return _context.SaveChangesAsync();
        }

        public async Task Update(int id, ProductEditorModel model)
        {
            var product = await _context.Products
                .Include(x => x.Image)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (product is null)
            {
                return;
            }

            product.Name = model.Name;
            product.Category = model.Category;
            product.Description = model.Description;
            product.Price = model.Price;

            if (model.Image is not null)
            {
                _context.ProductImages.RemoveRange(product.Image);
                _context.ProductImages.Add(new ProductImage
                {
                    ProductId = id,
                    Data = model.Image.Data,
                    Title = model.Image.Title
                });
            }

            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (product is null)
            {
                return;
            }

            _context.Products.SoftDelete(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<string>> GetCategories()
        {
            var categories = await _context.Products.Select(x => x.Category).Distinct().ToListAsync();
            return categories;
        }
    }
}
