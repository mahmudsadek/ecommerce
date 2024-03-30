using ecommerce.Models;
using ecommerce.Repository;

namespace ecommerce.Services
{
    public interface IOrderService  : IRepository<Order>  // to get the CRUD operations
    {
        // omar : add the extra methods if u want more than CRUD operations
    }
}