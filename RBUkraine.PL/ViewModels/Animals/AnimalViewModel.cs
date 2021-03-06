using System.ComponentModel.DataAnnotations;
using RBUkraine.PL.ViewModels.CharitableOrganizations;

namespace RBUkraine.PL.ViewModels.Animals
{
    public class AnimalViewModel
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

        public string Description { get; set; }

        public int Population { get; set; }

        public CharitableOrganizationViewModel CharitableOrganization { get; set; }

        [Display(Name = "Image")]
        public ImageViewModel Image { get; set; }
    }
}
