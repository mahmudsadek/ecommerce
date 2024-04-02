using ecommerce.Models;
using ecommerce.Repository;

namespace ecommerce.Services
{
    public interface IOrderItemService : IOrderItemRepository
    {
        List<OrderItem> Get(Func<Order, bool> where);
    }
}
