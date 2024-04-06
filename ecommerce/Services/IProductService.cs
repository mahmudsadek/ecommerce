using ecommerce.Models;
using ecommerce.Repository;

namespace ecommerce.Services
{
	public interface IProductService : IRepository<Product>
	{
		public List<Product> GetPageList(int skipstep, int pageSize);

	}
}