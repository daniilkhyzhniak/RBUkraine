using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using RBUkraine.BLL.Models.VolunteerApplication;

namespace RBUkraine.PL.ViewModels.VolunteerApplication
{
    public class VolunteerApplicationListViewModel
    {
        public IEnumerable<VolunteerApplicationModel> VolunteerApplications { get; set; }
        public IEnumerable<SelectListItem> CitiesSelectList { get; set; }

        public VolunteerApplicationFilterModel Filter { get; set; }
    }
}
