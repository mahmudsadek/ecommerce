using ecommerce.Models;

namespace ecommerce.Repository
{
    public class CartRepository : Repository<Cart> , ICartRepository
    {

        public CartRepository(Context context) : base(context) 
        {
            
        }

    }
}
