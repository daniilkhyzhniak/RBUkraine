using System.Collections.Generic;
using System.Threading.Tasks;
using RBUkraine.BLL.Enums;
using RBUkraine.BLL.Models.Product;

namespace RBUkraine.BLL.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> GetAll(string culture = Culture.Ukrainian);

        Task<IEnumerable<ProductModel>> GetAll(ProductFilterModel filter, string culture = Culture.Ukrainian);

        Task<ProductModel> Get(int id, string culture = Culture.Ukrainian);

        Task Create(ProductEditorModel model);

        Task Update(int id, ProductEditorModel model);

        Task Delete(int id);

        Task<IEnumerable<string>> GetCategories();
    }
}
