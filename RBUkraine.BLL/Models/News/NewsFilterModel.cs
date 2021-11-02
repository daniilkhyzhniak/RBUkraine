using System.Collections.Generic;
using RBUkraine.BLL.Enums;

namespace RBUkraine.BLL.Models.News
{
    public class NewsFilterModel
    {
        public NewsSearchOptions SearchOptions { get; set; } = NewsSearchOptions.ByTitle;

        public string Search { get; set; }

        public NewsSortOptions SortOptions { get; set; } = NewsSortOptions.ByDate;

        public SortDirection SortDirection { get; set; } = SortDirection.Asc;

        public IList<int> Founds { get; set; } = new List<int>();
    }
}
