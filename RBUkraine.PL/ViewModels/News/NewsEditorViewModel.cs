using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RBUkraine.PL.ViewModels.News
{
    public class NewsEditorViewModel
    {
        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string FullDescription { get; set; }

        public DateTimeOffset PublishDate { get; set; }

        public int? CharitableOrganizationId { get; set; }

        public int? AnimalId { get; set; }

        public IList<SelectListItem> CharitableOrganizationSelectList { get; set; }

        public IList<SelectListItem> AnimalSelectList { get; set; }
    }
}
