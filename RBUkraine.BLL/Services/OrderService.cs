using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RBUkraine.BLL.Contracts;
using RBUkraine.BLL.Models.Order;
using RBUkraine.DAL.Contexts;
using RBUkraine.DAL.Entities;

namespace RBUkraine.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(
            AppDbContext context)
        {
            _context = context;
        }

        public async Task MakeOrder(OrderCreationModel model)
        {
            if (!model.ProductsIds.Any())
            {
                throw new ArgumentException("Cart is empty");
            }

            var products = await _context.Products.Where(x => model.ProductsIds.Contains(x.Id)).ToListAsync();

            var order = new Order
            {
                UserId = model.UserId,
                Date = DateTimeOffset.Now
            };

            foreach (var product in products)
            {
                order.OrderDetails.Add(new OrderDetail
                {
                    ProductId = product.Id,
                    Price = product.Price
                });
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }
    }
}
