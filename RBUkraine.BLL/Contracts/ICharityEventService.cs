using System.Collections.Generic;
using System.Threading.Tasks;
using RBUkraine.BLL.Enums;
using RBUkraine.BLL.Models.CharityEvent;

namespace RBUkraine.BLL.Contracts
{
    public interface ICharityEventService
    {
        Task<IEnumerable<CharityEventModel>> GetAllAsync(string culture = Culture.Ukrainian);

        Task<IEnumerable<CharityEventModel>> GetAllAsync(CharityEventFilterModel filter, string culture = Culture.Ukrainian);

        Task<CharityEventModel> GetByIdAsync(int id, string culture = Culture.Ukrainian);

        Task DeleteAsync(int id);

        Task<int> CreateAsync(CharityEventEditorModel model);

        Task UpdateAsync(int id, CharityEventEditorModel model);
    }
}
