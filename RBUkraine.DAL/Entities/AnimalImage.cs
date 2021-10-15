﻿using RBUkraine.DAL.Entities.Base;

namespace RBUkraine.DAL.Entities
{
    public class AnimalImage : BaseEntity
    {
        public string Title { get; set; }

        public byte[] Data { get; set; }

        public int AnimalId { get; set; }
    }
}