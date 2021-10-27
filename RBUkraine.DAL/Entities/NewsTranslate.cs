using RBUkraine.DAL.Entities.Base;

namespace RBUkraine.DAL.Entities
{
    public class NewsTranslate : BaseTranslate
    {
        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string FullDescription { get; set; }

        public int NewsId { get; set; }

        public News News { get; set; }
    }
}
