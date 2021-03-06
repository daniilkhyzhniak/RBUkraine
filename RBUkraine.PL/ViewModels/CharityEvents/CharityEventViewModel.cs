using System;

namespace RBUkraine.PL.ViewModels.CharityEvents
{
    public class CharityEventViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTimeOffset DateTime { get; set; }

        public string Organizer { get; set; }

        public decimal Price { get; set; }
    }
}
