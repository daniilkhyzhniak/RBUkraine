using RBUkraine.BLL.Enums;

namespace RBUkraine.BLL.Models.CharityEvent
{
    public class CharityEventFilterModel
    {
        public CharityEventSearchOptions SearchOptions { get; set; } = CharityEventSearchOptions.ByTitle;

        public string Search { get; set; }

        public CharityEventSortOptions SortOptions { get; set; } = CharityEventSortOptions.ByTitle;

        public SortDirection SortDirection { get; set; } = SortDirection.Asc;
    }
}
