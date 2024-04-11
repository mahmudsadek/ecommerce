using ecommerce.Models;
using ecommerce.Repository;
using ecommerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService) 
        {
            this.categoryService = categoryService;
        }

        //*********************************************************

        [HttpGet]
        //[Route("/Dashbourd/categories")]
        public IActionResult GetAll(string? include = null)
        {
            List<Category> categories = categoryService.GetAll(include);

            return View(categories);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Category category = categoryService.Get(id);

            if (category != null)
            {
                return View("Get" ,category);
            }

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public IActionResult Get(Func<Category, bool> where)
        {
            List<Category> categories = categoryService.Get(where);

            return View(categories);
        }

        //--------------------------------------------

        [HttpGet]
        // [Authorize("Admin")]
        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // [Authorize("Admin")]
        public IActionResult Insert(Category category)
        {
            if (ModelState.IsValid)
            {
                categoryService.Insert(category);

                categoryService.Save();

                return RedirectToAction("categories","dashbourd");
            }

            return View(category);
        }

        //--------------------------------------------

        [HttpGet]
        // [Authorize("Admin")]
        public IActionResult Update(int id)
        {
            Category category = categoryService.Get(id);

            if (category != null)
            {
                return View(category);
            }

            return RedirectToAction("categories", "dashbourd");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //   [Authorize("Admin")]
        public IActionResult Update(Category category)
        {
            if (ModelState.IsValid)
            {
                categoryService.Update(category);

                categoryService.Save();

                return RedirectToAction("categories", "dashbourd");
            }

            return View(category);
        }

        //--------------------------------------------

        [HttpGet]
        // [Authorize("Admin")]
        public IActionResult Delete(int id)
        {
            Category category = categoryService.Get(id);

            if (category != null)
            {
                return View(category);
            }

            return RedirectToAction("categories", "dashbourd");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // [Authorize("Admin")]
        public IActionResult Delete(Category category)
        {
            categoryService.Delete(category);

            categoryService.Save();

            return RedirectToAction("categories", "dashbourd");
        }

        //--------------------------------------------



        // refaaey

        //public IActionResult Insert()
        //      {
        //          return View();
        //      }

        //      public IActionResult Update(int CategoryId)
        //      {
        //          return View("", categoryService.Get(CategoryId));
        //      }

        //      public IActionResult ShowCategoryProducts(int CategoryId)
        //      {
        //          Category category = categoryService.Get(CategoryId);

        //          return View("", category.Products);
        //      }


    }
}
