using System.Collections.Generic;

namespace RBUkraine.BLL.Models.Order
{
    public class OrderCreationModel
    {
        public int UserId { get; set; }

        public IEnumerable<int> ProductsIds { get; set; }
    }
}
