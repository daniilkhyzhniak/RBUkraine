using System.Collections.Generic;

namespace RBUkraine.PL.ViewModels.Animals
{
    public class AnimalDetailsViewModel
    {
        public int Id { get; set; }

        public string Species { get; set; }

        public string LatinSpecies { get; set; }

        public string ConservationStatus { get; set; }

        public string Kingdom { get; set; }

        public string Phylum { get; set; }

        public string Class { get; set; }

        public string Order { get; set; }

        public string Family { get; set; }

        public string Genus { get; set; }

        public int Population { get; set; }

        public ICollection<ImageViewModel> Images { get; set; }
    }
}
