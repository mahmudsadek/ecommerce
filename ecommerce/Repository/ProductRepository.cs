using ecommerce.Models;
using Microsoft.EntityFrameworkCore;
namespace ecommerce.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {

        public ProductRepository(Context _context) : base(_context)
        {
        }

        public void Insert(Product product)
        {
            Context.Add(product);
        }

        public void Update(Product product)
        {
            Context.Update(product);
        }

        public List<Product> GetAll(string include = null)
        {
            if (include == null)
            {
                return Context.Product.ToList();
            }
            return Context.Product.Include(include).ToList();
        }

        public Product Get(int Id)
        {
            return Context.Product.FirstOrDefault(p=>p.Id==Id);
        }
        public List<Product> Get(Func<Product, bool> where)
        {
            return Context.Product.Where(where).ToList();
        }

        public void Delete(Product product)
        {
            Context.Remove(product);
        }

        public void Save()
        {
            Context.SaveChanges();
        }
       


    }
}
