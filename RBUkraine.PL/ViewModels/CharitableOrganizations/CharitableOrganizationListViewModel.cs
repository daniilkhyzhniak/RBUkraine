using System.Collections.Generic;
using RBUkraine.BLL.Models.CharitableOrganization;

namespace RBUkraine.PL.ViewModels.CharitableOrganizations
{
    public class CharitableOrganizationListViewModel
    {
        public IList<CharitableOrganizationViewModel> CharitableOrganizations { get; set; }

        public CharitableOrganizationFilterModel Filter { get; set; }
    }
}
