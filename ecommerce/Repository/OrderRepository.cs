using ecommerce.Models;

namespace ecommerce.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(Context _context ) : base( _context )
        {

        }
    }
}