using RBUkraine.BLL.Models.VolunteerApplication;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RBUkraine.BLL.Contracts
{
    public interface IVolunteerApplicationService
    {
        public Task Create(VolunteerApplicationEditorModel model);
        public Task<IEnumerable<VolunteerApplicationModel>> GetAll(VolunteerApplicationFilterModel filter);
        public Task ChangeStatus(int id, bool confirmed);
        public Task<IEnumerable<string>> GetAllCities();
    }
}
