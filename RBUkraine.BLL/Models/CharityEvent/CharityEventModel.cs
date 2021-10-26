﻿using System;

namespace RBUkraine.BLL.Models.CharityEvent
{
    public class CharityEventModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTimeOffset DateTime { get; set; }

        public string Organizer { get; set; }

        public decimal Price { get; set; }
    }
}
