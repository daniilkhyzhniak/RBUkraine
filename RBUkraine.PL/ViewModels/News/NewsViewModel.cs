using System;
using RBUkraine.PL.ViewModels.Animals;
using RBUkraine.PL.ViewModels.CharitableOrganizations;

namespace RBUkraine.PL.ViewModels.News
{
    public class NewsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string FullDescription { get; set; }

        public DateTimeOffset PublishDate { get; set; }

        public int? CharitableOrganizationId { get; set; }

        public int? AnimalId { get; set; }

        public CharitableOrganizationViewModel CharitableOrganization { get; set; }

        public AnimalViewModel Animal { get; set; }
    }
}
