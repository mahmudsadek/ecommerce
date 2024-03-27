using ecommerce.Models;
namespace ecommerce.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(Context _context) : base(_context)
        {
        }

    }
}
