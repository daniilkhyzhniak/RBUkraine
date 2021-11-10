using System;
using System.ComponentModel.DataAnnotations;

namespace RBUkraine.PL.ViewModels.CharityEvents
{
    public class CharityEventEditorViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTimeOffset DateTime { get; set; }

        [Required]
        public string Organizer { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
