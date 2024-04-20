using ecommerce.Models;
using ecommerce.Repository;

namespace ecommerce.Services
{
    public interface ICategoryService : IRepository<Category>
    {
        public List<Category> GetPageList(int skipstep, int pageSize);

        public Category Get(int id, string? include = null);

        public List<Product> GetAllProductsInCategory(int CategoryId);

    }
}