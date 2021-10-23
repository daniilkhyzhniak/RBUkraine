using RBUkraine.BLL.Enums;

namespace RBUkraine.BLL.Models.Animal
{
    public class AnimalFilterModel
    {
        public AnimalsSearchOptions SearchOptions { get; set; } = AnimalsSearchOptions.BySpecious;
        
        public string Search { get; set; }

        public AnimalsSortOptions SortOptions { get; set; }

        public SortDirection SortDirection { get; set; }
    }
}
