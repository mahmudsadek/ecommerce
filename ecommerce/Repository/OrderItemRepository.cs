using ecommerce.Models;

namespace ecommerce.Repository
{
    public class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository
    {
        private readonly Context context;

        public OrderItemRepository(Context _context) : base(_context)
        {
            context = _context;
        }
    }
}
