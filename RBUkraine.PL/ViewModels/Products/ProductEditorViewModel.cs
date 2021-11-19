﻿using Microsoft.AspNetCore.Http;

namespace RBUkraine.PL.ViewModels.Products
{
    public class ProductEditorViewModel
    {
        public string Name { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public IFormFile File { get; set; }
    }
}
