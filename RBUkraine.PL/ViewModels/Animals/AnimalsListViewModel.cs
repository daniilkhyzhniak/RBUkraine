using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using RBUkraine.BLL.Models.Animal;

namespace RBUkraine.PL.ViewModels.Animals
{
    public class AnimalsListViewModel
    {
        public IList<AnimalViewModel> Animals { get; set; }

        public AnimalFilterModel Filter { get; set; }

        public IEnumerable<SelectListItem> FoundSelectList { get; set; }
    }
}
