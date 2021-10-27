namespace RBUkraine.BLL.Models.CharitableOrganization
{
    public class CharitableOrganizationTranslateEditorModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Founders { get; set; }

        public string Stockholders { get; set; }

        public string Culture { get; set; } = Enums.Culture.English;
    }
}
