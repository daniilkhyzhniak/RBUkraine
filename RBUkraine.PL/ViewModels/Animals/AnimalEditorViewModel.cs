using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RBUkraine.PL.ViewModels.Animals
{
    public class AnimalEditorViewModel
    {
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

        public string Description { get; set; }

        public ImageViewModel Image { get; set; }

        public IFormFileCollection Files { get; set; }

        public int CharitableOrganizationId { get; set; }

        public IList<SelectListItem> CharitableOrganizations { get; set; }
    }
}
