using ecommerce.Models;
using ecommerce.Services;
using ecommerce.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Controllers
{
	public class ProductController : Controller
	{
		private readonly IProductService productService;

		public ICategoryService categoryService { get; }

		public ProductController(IProductService productService, ICategoryService categoryService)
		{
			this.productService = productService;

			this.categoryService = categoryService;
		}

		//********************************************************


		[HttpGet]
		public IActionResult GetAll(string? include = null)
		{
			Products_With_CategoriesVM products_CategoriesVM = new Products_With_CategoriesVM()
			{
				Products = productService.GetAll(include),
				Categories = categoryService.GetAll(include)
			};

			return View(products_CategoriesVM);
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
