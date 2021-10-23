using RBUkraine.BLL.Models.Animal;
using System.Collections.Generic;
using System.Threading.Tasks;
using RBUkraine.BLL.Enums;

namespace RBUkraine.BLL.Contracts
{
    public interface IAnimalService
    {
        Task<IEnumerable<AnimalModel>> GetAllAsync(AnimalFilterModel filter, string culture = Culture.Ukrainian);

        Task<AnimalModel> GetByIdAsync(int id, string culture = Culture.Ukrainian);

        Task<int> CreateAnimalAsync(AnimalEditorModel model);

        Task UpdateAnimalAsync(int id, AnimalEditorModel model);

        Task DeleteAnimalAsync(int id);
        
        Task AddOrUpdateAnimalTranslateAsync(int animalId, AnimalTranslateEditorModel model);
    }
}
