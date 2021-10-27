using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RBUkraine.PL.ViewModels.Animals
{
    public class AnimalEditorViewModel
    {
        public int Id { get; set; }

        [Required]

        public string Species { get; set; }

        [Required]
        public string LatinSpecies { get; set; }

        [Required]
        public string ConservationStatus { get; set; }

        public string Kingdom { get; set; }

        public string Phylum { get; set; }

        public string Class { get; set; }

        public string Order { get; set; }

        public string Family { get; set; }

        public string Genus { get; set; }

        [Required]
        public int Population { get; set; }

        [Required]
        public string Description { get; set; }

        public ImageViewModel Image { get; set; }

        public IFormFileCollection Files { get; set; }

        public int CharitableOrganizationId { get; set; }

        public IList<SelectListItem> CharitableOrganizations { get; set; }
    }
}
