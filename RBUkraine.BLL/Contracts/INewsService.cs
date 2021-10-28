using System.Collections.Generic;
using System.Threading.Tasks;
using RBUkraine.BLL.Enums;
using RBUkraine.BLL.Models.News;

namespace RBUkraine.BLL.Contracts
{
    public interface INewsService
    {
        Task<IEnumerable<NewsModel>> GetAllAsync(string culture = Culture.Ukrainian);

        Task<NewsModel> GetByIdAsync(int id, string culture = Culture.Ukrainian);

        Task DeleteAsync(int id);

        Task<int> CreateAsync(NewsEditorModel model);

        Task UpdateAsync(int id, NewsEditorModel model);
    }
}
