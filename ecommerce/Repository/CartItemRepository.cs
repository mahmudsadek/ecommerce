using ecommerce.Models;

namespace ecommerce.Repository
{
    public class CartItemRepository : Repository<CartItem> , ICartItemRepository
    {

        public CartItemRepository(Context context) : base(context)
        {
            
        }

    }
}
