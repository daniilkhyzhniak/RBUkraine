using Microsoft.AspNetCore.Http;

namespace RBUkraine.PL.ViewModels.Animals
{
    public class AnimalEditorViewModel
    {
        public string Name { get; set; }
        
        public string Description { get; set; }

        public IFormFileCollection Files { get; set; }
    }
}
