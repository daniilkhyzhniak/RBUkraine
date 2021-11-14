using System.Threading.Tasks;
using RBUkraine.BLL.Models.Order;

namespace RBUkraine.BLL.Contracts
{
    public interface IOrderService
    {
        Task MakeOrder(OrderCreationModel model);
    }
}
