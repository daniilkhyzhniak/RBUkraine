using System.Collections.Generic;
using RBUkraine.BLL.Models.News;

namespace RBUkraine.PL.ViewModels.News
{
    public class NewsListViewModel
    {
        public IList<NewsViewModel> News { get; set; }

        public NewsFilterModel Filter { get; set; }
    }
}
