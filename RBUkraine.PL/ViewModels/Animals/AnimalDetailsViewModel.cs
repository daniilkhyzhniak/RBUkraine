using System.Collections.Generic;

namespace RBUkraine.PL.ViewModels.Animals
{
    public class AnimalDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LatinName { get; set; }

        public string Description { get; set; }

        public int Population { get; set; }

        public ICollection<ImageViewModel> Images { get; set; }
    }
}
