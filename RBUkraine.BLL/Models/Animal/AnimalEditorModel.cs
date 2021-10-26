using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RBUkraine.BLL.Models.Animal
{
    public class AnimalEditorModel
    {
        [Required]
        public string Species { get; set; }

        [Required]
        public string LatinSpecies { get; set; }

        [Required]
        public string ConservationStatus { get; set; }

        [Required]
        public string Kingdom { get; set; }

        public string Phylum { get; set; }

        public string Class { get; set; }

        public string Order { get; set; }

        public string Family { get; set; }

        public string Genus { get; set; }

        public string Description { get; set; }

        public int Population { get; set; }

        public int CharitableOrganizationId { get; set; }

        public ICollection<Image> Images { get; set; }
    }
}
