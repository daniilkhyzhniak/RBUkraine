using System.Collections.Generic;
using RBUkraine.BLL.Models.CharityEvent;

namespace RBUkraine.PL.ViewModels.CharityEvents
{
    public class CharityEventListViewModel
    {
        public IList<CharityEventViewModel> CharityEvents { get; set; }

        public CharityEventFilterModel Filter { get; set; }
    }
}
