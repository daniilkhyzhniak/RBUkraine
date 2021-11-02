using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using RBUkraine.BLL.Models.News;

namespace RBUkraine.PL.ViewModels.News
{
    public class NewsListViewModel
    {
        public IList<NewsViewModel> News { get; set; }

        public NewsFilterModel Filter { get; set; }

        public IEnumerable<SelectListItem> FoundSelectList { get; set; }
    }
}
