using System.Collections.Generic;
using RBUkraine.BLL.Enums;

namespace RBUkraine.BLL.Models.Animal
{
    public class AnimalFilterModel
    {
        public AnimalsSearchOptions SearchOptions { get; set; } = AnimalsSearchOptions.BySpecious;
        
        public string Search { get; set; }

        public AnimalsSortOptions SortOptions { get; set; } = AnimalsSortOptions.BySpecies;

        public SortDirection SortDirection { get; set; } = SortDirection.Asc;

        public IList<int> Founds { get; set; } = new List<int>();
    }
}
