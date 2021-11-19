using System;
using Microsoft.AspNetCore.Http;

namespace RBUkraine.PL.ViewModels.CharitableOrganizations
{
    public class CharitableOrganizationEditorViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Founders { get; set; }

        public string Stockholders { get; set; }
        
        public DateTimeOffset FoundationDate { get; set; }

        public IFormFile File { get; set; }
    }
}
