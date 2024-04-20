using ecommerce.Models;
namespace ecommerce.ViewModels.Category
{
    public class CategoryWithProducts
    {

        public int SelectedCategoryId { get; set; }
        public List<ecommerce.Models.Product> Products { get; set; }
        public List<Models.Category> Categories { get; set; }




    }
}
