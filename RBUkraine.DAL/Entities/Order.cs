using RBUkraine.DAL.Entities.Base;
using System;
using System.Collections.Generic;

namespace RBUkraine.DAL.Entities
{
    public class Order : BaseEntity
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public DateTimeOffset Date { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
