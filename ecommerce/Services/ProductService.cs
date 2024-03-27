using ecommerce.Repository;

namespace ecommerce.Services
{
    public class ProductService
    {
        private readonly IProductRepository repository;
        public ProductService(IProductRepository _repository)
        {
            repository = _repository;
        }

    }
}
