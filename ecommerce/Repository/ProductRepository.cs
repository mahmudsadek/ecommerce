using ecommerce.Models;
namespace ecommerce.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(Context _context) : base(_context)
        {
            // Omar : implement the methods u declared in IProductRepository
            // and if u added method here don't forget to declare it first in the interface
        }
    }
}
