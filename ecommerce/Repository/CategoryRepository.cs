using ecommerce.Models;

namespace ecommerce.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(Context _context) : base(_context)
        {
        }

    }
}
