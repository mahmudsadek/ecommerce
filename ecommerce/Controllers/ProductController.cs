using ecommerce.Models;
using ecommerce.Services;
using ecommerce.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using System.Drawing.Printing;

namespace ecommerce.Controllers
{
	public class ProductController : Controller
	{
		private readonly IProductService productService;

		public ICategoryService categoryService { get; }

		private const int _pageSize = 2;

		public ProductController(IProductService productService, ICategoryService categoryService)
		{
			this.productService = productService;

			this.categoryService = categoryService;
		}

		//********************************************************


		[HttpGet]
		public IActionResult GetAll(int page = 1 , int pageSize = _pageSize)
		{
			int skipStep = (page - 1) * pageSize;

			List<Product> PaginatedProducts = productService.GetPageList(skipStep, pageSize);

			int productsCount = productService.GetAll().Count();

			ViewData["TotalPages"] = Math.Ceiling(productsCount / (double)pageSize);

			ViewData["AllProductsNames"] = productService.GetAll().Select(c => c.Name).ToList();

			Products_With_CategoriesVM products_CategoriesVM = new Products_With_CategoriesVM()
			{
				Products = PaginatedProducts ,
				Categories = categoryService.GetAll(),
			};

			return View(products_CategoriesVM);
		}

        [HttpGet]
        public IActionResult GetAllPartial(int[] catedIds , int page = 1, int pageSize = _pageSize)
        {
            int skipStep = (page - 1) * pageSize;

            List<Product> PaginatedProducts = productService.GetPageList(skipStep, pageSize)
				.Where(p => catedIds.Contains(p.CategoryId)).ToList();

            int productsCount = productService.GetAll().Where(p => catedIds.Contains(p.CategoryId)).Count();

            ViewData["TotalPages"] = Math.Ceiling(productsCount / (double)pageSize);

            ViewData["AllProductsNames"] = productService.GetAll().Select(c => c.Name).ToList();

            Products_With_CategoriesVM products_CategoriesVM = new Products_With_CategoriesVM()
            {
                Products = PaginatedProducts,
                Categories = categoryService.GetAll(),
            };

            return PartialView("_ProductsPartial", products_CategoriesVM);
        }

        //[HttpGet]
		public IActionResult GetAllFiltered(int minPrice, int maxPrice , int[] categIds , int page = 1 , int pageSize = _pageSize )
		{
			if (categIds == null)
			{
                categIds = categoryService.GetAll().Select(c => c.Id).ToArray();
            }

            int skipStep = (page - 1) * pageSize;

			List<Product> categAllProducts = productService.GetAll()
				.Where(p => categIds.Contains(p.CategoryId))
				.Where(p => p.Price >= minPrice && p.Price <= maxPrice)
				.ToList() ;

            List<Product> PaginatedCategProducts = categAllProducts.Skip(skipStep).Take(pageSize).ToList();

			int productsCount = categAllProducts.Count();

            ViewData["TotalPages"] = Math.Ceiling(productsCount / (double)pageSize);

            ViewData["AllProductsNames"] = productService.GetAll().Select(c => c.Name).ToList();

            Products_With_CategoriesVM products_CategoriesVM = new Products_With_CategoriesVM()
            {
                Products = PaginatedCategProducts,

                Categories = categoryService.GetAll(),
            };

            return PartialView("_ProductsPartial", products_CategoriesVM);
		}

        public IActionResult Search(string searchProdName )
        {
            List<Product> searchedProducts = productService.GetAll()
                .Where(p => p.Name.Contains(searchProdName)).ToList();

            Products_With_CategoriesVM products_CategoriesVM = new Products_With_CategoriesVM()
            {
                Products = searchedProducts,

                Categories = categoryService.GetAll(),
            };

            ViewData["TotalPages"] = Math.Ceiling(productService.GetAll().Count() / (double)_pageSize);

            ViewData["AllProductsNames"] = productService.GetAll().Select(c => c.Name).ToList();

            return View("GetAll", products_CategoriesVM);
        }

        [HttpGet]
		public IActionResult Details(int id)
		{
			Product productDB = productService.Get(id);

			if (productDB != null)
			{
				Category prodCateg = categoryService.Get(productDB.CategoryId);

				Product_With_RelatedProducts prodVM = new Product_With_RelatedProducts()
				{
					Id = productDB.Id,
					Name = productDB.Name,
					Description = productDB.Description,
					Price = productDB.Price,
					Quantity = productDB.Quantity,
					ImageUrl = productDB.ImageUrl,
					Rating = productDB.Rating,
					Color = productDB.Color,

					CategoryId = productDB.CategoryId,
					Category = prodCateg,

					CategoryName = prodCateg.Name,

					Comments = productDB.Comments,

					RealtedProducts = productService.Get(p => p.CategoryId == productDB.CategoryId)
				};
				return View("Get", prodVM);
			}

			return RedirectToAction("GetAll");
		}

		[HttpGet]
		public IActionResult Get(Func<Product, bool> where)
		{
			List<Product> products = productService.Get(where);

			return View(products);
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
		public IActionResult Insert(Product product)
		{
			if (ModelState.IsValid)
			{
				productService.Insert(product);

				productService.Save();

				return RedirectToAction("GetAll");
			}

			return View(product);
		}

		//--------------------------------------------

		[HttpGet]
		// [Authorize("Admin")]
		public IActionResult Update(int id)
		{
			Product product = productService.Get(id);

			if (product != null)
			{
				return View(product);
			}

			return RedirectToAction("GetAll");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		//   [Authorize("Admin")]
		public IActionResult Update(Product product)
		{
			if (ModelState.IsValid)
			{
				productService.Update(product);

				productService.Save();

				return RedirectToAction("GetAll");
			}

			return View(product);
		}

		//--------------------------------------------

		[HttpGet]
		// [Authorize("Admin")]
		public IActionResult Delete(int id)
		{
			Product product = productService.Get(id);

			if (product != null)
			{
				return View(product);
			}

			return RedirectToAction("GetAll");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		// [Authorize("Admin")]
		public IActionResult Delete(Product product)
		{
			productService.Delete(product);

			productService.Save();

			return RedirectToAction("GetAll");
		}

		//--------------------------------------------
	}
}
