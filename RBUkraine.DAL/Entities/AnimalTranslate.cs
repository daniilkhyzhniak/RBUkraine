using RBUkraine.DAL.Entities.Base;

namespace RBUkraine.DAL.Entities
{
    public class AnimalTranslate : BaseTranslate
    {
        public int AnimalId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Animal Animal { get; set; }
    }
}
