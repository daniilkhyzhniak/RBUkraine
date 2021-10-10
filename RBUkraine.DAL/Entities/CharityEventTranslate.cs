using RBUkraine.DAL.Entities.Base;

namespace RBUkraine.DAL.Entities
{
    public class CharityEventTranslate : BaseTranslate
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Organizer { get; set; }

        public int CharityEventId { get; set; }

        public CharityEvent CharityEvent { get; set; }
    }
}
