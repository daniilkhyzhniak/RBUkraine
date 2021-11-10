using System;
using RBUkraine.BLL.Models.Animal;
using RBUkraine.BLL.Models.CharitableOrganization;

namespace RBUkraine.BLL.Models.News
{
    public class NewsModel
    {
        // public int Id { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string FullDescription { get; set; }

        public DateTimeOffset PublishDate { get; set; }

        public int? CharitableOrganizationId { get; set; }

        public int? AnimalId { get; set; }

        public CharitableOrganizationModel CharitableOrganization { get; set; }

        public AnimalModel Animal { get; set; }
    }
}
