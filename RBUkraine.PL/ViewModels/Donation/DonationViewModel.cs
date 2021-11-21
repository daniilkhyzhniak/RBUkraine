using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace RBUkraine.PL.ViewModels.Donation
{
    public class DonationViewModel
    {
        public int? AnimalId { get; set; }

        public int? CharitableOrganizationId { get; set; }
        
        public decimal Amount { get; set; }

        public IEnumerable<SelectListItem> Species = new List<SelectListItem>();

        public IEnumerable<SelectListItem> Fonds = new List<SelectListItem>();
    }
}
