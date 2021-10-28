using RBUkraine.BLL.Enums;

namespace RBUkraine.BLL.Models.News
{
    public class NewsFilterModel
    {
        public NewsSearchOptions SearchOptions { get; set; } = NewsSearchOptions.ByTitle;

        public string Search { get; set; }

        public CharityEventSortOptions SortOptions { get; set; } = CharityEventSortOptions.ByDate;

        public SortDirection SortDirection { get; set; } = SortDirection.Asc;
    }
}
