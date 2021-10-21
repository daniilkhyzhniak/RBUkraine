using System;
using System.Collections.Generic;
using RBUkraine.BLL.Models.Animal;

namespace RBUkraine.BLL.Models.CharitableOrganization
{
    public class CharitableOrganizationModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string Email { get; set; }

        public string Founders { get; set; }

        public string Stockholders { get; set; }

        public DateTimeOffset FoundationDate { get; set; }

        public IEnumerable<AnimalModel> Animals { get; set; }

        public Image Image { get; set; }
    }
}
