using System;

namespace RBUkraine.BLL.Models.CharitableOrganization
{
    public class CharitableOrganizationEditorModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Founders { get; set; }

        public string Stockholders { get; set; }

        public DateTimeOffset FoundationDate { get; set; }

        public Image Image { get; set; }
    }
}
