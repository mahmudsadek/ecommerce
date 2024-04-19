using ecommerce.Models;

namespace ecommerce.Repository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        List<Category> GetAll(string include = null);

        public List<Product> GetAllProductsInCategory(int CategoryId);

        Category Get(int id, string include);

        Category GetCategoryByName(string name);

        List<Category> Get(Func<Category, bool> where);

        void Insert(Category item);

        //I think its related to products
        //void ChangeProductCategory(Product item, int old_category_id, int new_category_id); // To transfer product from category to another

        void TransferAllProductsToAnotherCategory(int OldCategoryId, int NewCategoryId);

        void DeleteAllProductsInCategory(int CategoryId);

        void Update(Category item);

        void Delete(int id);

        void Save();

        public List<Category> GetPageList(int skipstep, int pageSize);
    }
}
