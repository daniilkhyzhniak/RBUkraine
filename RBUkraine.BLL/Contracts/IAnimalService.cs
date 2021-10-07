using RBUkraine.BLL.Models.Animal;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RBUkraine.BLL.Contracts
{
    public interface IAnimalService
    {
        Task<IEnumerable<AnimalModel>> GetAllAsync();

        Task<AnimalDetailsModel> GetByIdAsync(int id);

        Task<int> CreateAnimalAsync(AnimalEditorModel model);

        Task UpdateAnimalAsync(int id, AnimalEditorModel model);

        Task DeleteAnimalAsync(int id);
    }
}
