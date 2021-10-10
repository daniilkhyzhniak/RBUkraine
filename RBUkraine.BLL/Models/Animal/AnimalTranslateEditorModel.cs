namespace RBUkraine.BLL.Models.Animal
{
    public class AnimalTranslateEditorModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Culture { get; set; } = Enums.Culture.English;
    }
}
