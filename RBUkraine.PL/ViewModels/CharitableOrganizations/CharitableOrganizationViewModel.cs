using System.Collections.Generic;
using RBUkraine.PL.ViewModels.Animals;

namespace RBUkraine.PL.ViewModels.CharitableOrganizations
{
    public class CharitableOrganizationViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public IEnumerable<AnimalViewModel> Animals { get; set; }
    }
}
