using System.Collections.Generic;
using System.Threading.Tasks;
using RBUkraine.BLL.Enums;
using RBUkraine.BLL.Models.CharitableOrganization;

namespace RBUkraine.BLL.Contracts
{
    public interface ICharitableOrganizationService
    {
        Task<IEnumerable<CharitableOrganizationModel>> GetAllWithoutAnimalsAsync(string culture = Culture.Ukrainian);

        Task<IEnumerable<CharitableOrganizationModel>> GetAllAsync(string culture = Culture.Ukrainian);

        Task<CharitableOrganizationModel> GetByIdAsync(int id, string culture = Culture.Ukrainian);

        Task DeleteAsync(int id);

        Task<int> CreateAsync(CharitableOrganizationEditorModel model);

        Task UpdateAsync(int id, CharitableOrganizationEditorModel model);
    }
}
