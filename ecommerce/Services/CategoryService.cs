using ecommerce.Repository;

namespace ecommerce.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository repository;
        public CategoryService(ICategoryRepository _repository)
        {
            repository = _repository;
        }
    }
}
