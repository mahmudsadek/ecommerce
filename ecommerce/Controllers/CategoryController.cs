using ecommerce.Models;
using ecommerce.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository CategoryRepository;
        public CategoryController(ICategoryRepository CategoryRepository) 
        {
            this.CategoryRepository = CategoryRepository;
        }


        public IActionResult Index(string include = null)
        {
            CategoryRepository.GetAll(include);

            return View();
        }


        public IActionResult Insert()
        {
            return View();
        }

        public IActionResult Update(int CategoryId)
        {
            return View("", CategoryRepository.Get(CategoryId));
        }

        public IActionResult ShowCategoryProducts(int CategoryId)
        {
            Category category = CategoryRepository.Get(CategoryId);

            return View("", category.Products);
        }


    }
}
