namespace RBUkraine.DAL.Entities.Base
{
    public class BaseImage : BaseEntity
    {
        public string Title { get; set; }

        public byte[] Data { get; set; }
    }
}
