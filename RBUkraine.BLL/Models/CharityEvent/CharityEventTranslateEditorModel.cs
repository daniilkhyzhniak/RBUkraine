namespace RBUkraine.BLL.Models.CharityEvent
{
    public class CharityEventTranslateEditorModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string Organizer { get; set; }

        public string Culture { get; set; } = Enums.Culture.English;
    }
}
