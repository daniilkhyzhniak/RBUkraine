using System.Collections.Generic;

namespace RBUkraine.BLL.Models.Animal
{
    public class AnimalEditorModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Image> Images { get; set; }
    }
}
