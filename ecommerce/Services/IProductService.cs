using ecommerce.Models;
using ecommerce.Repository;
using ecommerce.ViewModels.Product;

namespace ecommerce.Services
{
	public interface IProductService : IRepository<Product>
	{
		public List<Product> GetPageList(int skipstep, int pageSize);
		public  Task<ProductWithCatNameAndComments> WithCatNameAndComments(int id);
		public void Insert(ProductWithListOfCatesViewModel p);
        public void Update(ProductWithListOfCatesViewModel p);

        public ProductWithListOfCatesViewModel GetViewModel(int id);


    }
}