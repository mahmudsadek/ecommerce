using ecommerce.Models;
using ecommerce.Repository;
using ecommerce.ViewModels.Product;

namespace ecommerce.Services
{
	public interface IProductService : IRepository<Product>
	{
		public List<Product> GetPageList(int skipstep, int pageSize);
		public  Task<ProductWithCatNameAndComments> WithCatNameAndComments(int id);


    }
}