using ecommerce.Models;
namespace ecommerce.ViewModels.Category
{
    public class CategoryWithProducts
    {
        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        public List<ecommerce.Models.Product> Products { get; set; }



    }
}
