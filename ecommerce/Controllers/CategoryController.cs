using ecommerce.Models;
using ecommerce.Repository;
using ecommerce.Services;
using ecommerce.ViewModels.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CategoryController(ICategoryService categoryService,IWebHostEnvironment web) 
        {
            this.categoryService = categoryService;
            this._webHostEnvironment = web;

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

        public IActionResult ShowProducts(Category category)
        {
            CategoryWithProducts CategoryVM = new CategoryWithProducts()
            {
                SelectedCategoryId = category.Id,
                Products = categoryService.GetAllProductsInCategory(category.Id),
                Categories = categoryService.GetAll()
            };

            return View(CategoryVM);

        }

        [HttpGet]
        public IActionResult Get(Func<Category, bool> where)
        {
            List<Category> categories = categoryService.Get(where);

            return View(categories);
        }

        //--------------------------------------------

        [HttpGet]
        //[Authorize("Admin")]
        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        public IActionResult Insert(Category category)
        {
            string uploadpath = Path.Combine(_webHostEnvironment.WebRootPath, "img");
            string imagename = Guid.NewGuid().ToString() + "_" + category.image.FileName;
            string filepath = Path.Combine(uploadpath, imagename);
            using (FileStream fileStream = new FileStream(filepath, FileMode.Create))
            {
                category.image.CopyTo(fileStream);
            }
            category.ImageUrl = imagename;

            if (ModelState.IsValid)
            {
                categoryService.Insert(category);

                categoryService.Save();

                return RedirectToAction("categories", "dashbourd");
            }

            return View(category);
        }

        //--------------------------------------------

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public IActionResult Update(int id)
        {
            Category category = categoryService.Get(id, "Products");

            if (category != null)
            {
                return View(category);
            }

            return RedirectToAction("categories", "dashbourd");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        public IActionResult Update(Category category)
        {
            string uploadpath = Path.Combine(_webHostEnvironment.WebRootPath, "img");
            string imagename = Guid.NewGuid().ToString() + "_" + category.image.FileName;
            string filepath = Path.Combine(uploadpath, imagename);
            using (FileStream fileStream = new FileStream(filepath, FileMode.Create))
            {
                category.image.CopyTo(fileStream);
            }
            category.ImageUrl = imagename;
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
        //[Authorize(Roles = "Admin")]
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
        //[Authorize(Roles = "Admin")]
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
