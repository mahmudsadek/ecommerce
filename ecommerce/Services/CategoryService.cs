using ecommerce.Models;
using ecommerce.Repository;

namespace ecommerce.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryService(ICategoryRepository _repository)
        {
            categoryRepository = _repository;
        }

        public List<Category> GetAll(string include = null)
        {
            return categoryRepository.GetAll(include);  // the base function handles the null with if condition
        }

        public Category Get(int id)
        {
            return categoryRepository.Get(id);
        }

        public Category Get(int id, string? include = null)
        {
            if (include != null)
            {
                return categoryRepository.Get(id, include);
            }
            return categoryRepository.Get(id);
        }

        public List<Category> Get(Func<Category, bool> where)
        {
            return categoryRepository.Get(where);
        }

        public void Insert(Category category)
        {
            categoryRepository.Insert(category);
            categoryRepository.Save();
        }

        public void Update(Category updatedCategory)
        {
            Category category = Get(updatedCategory.Id);

            categoryRepository.Update(category);
        }

        public void Delete(Category category)
        {
            categoryRepository.Delete(category);
        }

        public void Save()
        {
            categoryRepository.Save();
        }

        public List<Product> GetAllProductsInCategory(int CategoryId)
        {
            return categoryRepository.GetAllProductsInCategory(CategoryId);
        }

        public List<Category> GetPageList(int skipstep, int pageSize)
        {
            return categoryRepository.GetPageList(skipstep, pageSize);
        }
    }
}
