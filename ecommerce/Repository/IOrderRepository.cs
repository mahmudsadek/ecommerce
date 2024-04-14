using ecommerce.Models;

namespace ecommerce.Repository
{
    public interface IOrderRepository : IRepository<Order>
    {
        // Omar : any logic specified to Order Only more than CRUD
        Order InsertOrder(Order order);
    }
}
