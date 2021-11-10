using System;

namespace RBUkraine.BLL.Models.News
{
    public class NewsEditorModel
    {
        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string FullDescription { get; set; }

        public DateTimeOffset PublishDate { get; set; }

        public int? CharitableOrganizationId { get; set; }

        public int? AnimalId { get; set; }
    }
}
