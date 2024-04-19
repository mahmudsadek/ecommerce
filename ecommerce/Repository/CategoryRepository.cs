using ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ecommerce.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        Context Context;
        public CategoryRepository(Context _context) : base(_context)
        {
            Context = _context;
        }

        public List<Category> GetAll(string include = null)
        {
            if (include != null)
            {
                return Context.Category.Include(include).ToList();
            }

            return Context.Category.ToList();
        }

        public List<Product> GetAllProductsInCategory(int CategoryId)
        {
            Category category = Get(CategoryId, "products");

            return category.Products;
        }

        public Category Get(int id, string? include =null)
        {
            if(include != null)
            {
                Context.Category.Include(include).FirstOrDefault(c => c.Id == id);
            }
            return Context.Category.FirstOrDefault(c => c.Id == id);
        }

        public Category GetCategoryByName(string name)
        {
            return Context.Category.FirstOrDefault(c => c.Name == name);
        }

        List<Category> Get(Func<Category, bool> where)
        {
            return Context.Category.Where(where).ToList();
        }

        public void Insert(Category item)
        {
            Context.Add(item);
            item.Products = new List<Product>();
        }

        //void ChangeProductCategory(int product_id, int old_category_id, int new_category_id)
        //{
        //    Product product = Get(product_id);
        //}

        public void TransferAllProductsToAnotherCategory(int OldCategoryId, int NewCategoryId) // The id of the new category
        {

            List<Product> products = Get(OldCategoryId).Products;

            foreach (Product product in products)
            {
                product.CategoryId = NewCategoryId;
            }
        }

        // THERE IS NO NEED FOR IT
        //public void RenameCategory(string categoryName, string NewName)
        //{
        //    GetCategoryByName(categoryName).Name = NewName;
        //}

        public void DeleteAllProductsInCategory(int CategoryId)
        { 
            Category Category = Get(CategoryId);
            List<Product> products = Category.Products.ToList();

            foreach (Product product in products)
            {
                Context.Remove(product);
            }
        }

        public void Update(Category item)
        {
            Context.Update(item);
        }

        public void Delete(int id)
        {
            Category item = Get(id);

            if(item.Products.Count == 0) // To Check if the category is empty before delete it
            {
                Context.Remove(item);
            }
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public List<Category> GetPageList(int skipstep, int pageSize)
        {
            return Context.Category.Skip(skipstep).Take(pageSize).ToList();
        }
    }
}
