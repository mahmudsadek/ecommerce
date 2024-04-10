using ecommerce.Models;

namespace ecommerce.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        List<Product> GetAll(string include = null);

        Product Get(int id);

        List<Product> Get(Func<Product, bool> where);

        void Insert(Product item);

        void Update(Product item);

        void Delete(Product item);

        void Save();

        public List<Product> GetPageList(int skipstep, int pageSize);
	}
}
