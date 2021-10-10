using System.ComponentModel.DataAnnotations;

namespace RBUkraine.PL.ViewModels.Animals
{
    public class AnimalViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        public string LatinName { get; set; }

        public string Description { get; set; }

        public int Population { get; set; }

        [Display(Name = "Image")]
        public ImageViewModel Image { get; set; }
    }
}
