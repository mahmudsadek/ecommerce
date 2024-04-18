using ecommerce.Models;
using ecommerce.Repository;

namespace ecommerce.Services
{
    public interface IOrderService  : IRepository<Order>  // to get the CRUD operations => and u can see them in the controller because u are using IOrderService ref in the controller
    {
        // omar : add the extra methods if u want more than CRUD operations
        Order InsertOrder(Order order);
    }
}