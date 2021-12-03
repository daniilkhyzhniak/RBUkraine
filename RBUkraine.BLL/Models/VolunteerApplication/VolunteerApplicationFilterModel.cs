using System.Collections.Generic;
using RBUkraine.BLL.Enums;

namespace RBUkraine.BLL.Models.VolunteerApplication
{
    public class VolunteerApplicationFilterModel
    {
        public string Search { get; set; }
        public SortDirection SortDirection { get; set; } = SortDirection.Asc;
        public IList<string> Cities { get; set; } = new List<string>();
    }
}
