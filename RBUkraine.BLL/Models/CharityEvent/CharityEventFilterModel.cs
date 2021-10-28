using RBUkraine.BLL.Enums;

namespace RBUkraine.BLL.Models.CharityEvent
{
    public class CharityEventFilterModel
    {
        public CharityEventSearchOptions SearchOptions { get; set; } = CharityEventSearchOptions.ByTitle;

        public string Search { get; set; }

        public CharityEventSortOptions SortOptions { get; set; } = CharityEventSortOptions.ByDate;

        public SortDirection SortDirection { get; set; } = SortDirection.Asc;
    }
}
