using ecommerce.Models;
using ecommerce.Services;
using ecommerce.ViewModels.Home;
using ecommerce.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace ecommerce.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ICartService cartService;
		private readonly IProductService productService;
		private readonly ICategoryService categoryService;

		public HomeController
			(ILogger<HomeController> logger, ICartService cartService ,
			IProductService productService , ICategoryService categoryService)
		{
			_logger = logger;
			this.cartService = cartService;
			this.productService = productService;
			this.categoryService = categoryService;
		}

		public IActionResult Index()
		{
			List<Product> products = productService.GetAll("Category");

			List<Cart> carts = cartService.GetAll();

			// Get the user ID
			string userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

			ViewBag.UserId = userIdClaim;

			ViewBag.AllProductsNames = productService.GetAll().Select(p => p.Name);

			if (carts.Count == 0)
			{
				Cart cart = new Cart() { CartItems = new List<CartItem>()};

				Prod_Cat_Cart_VM model = new Prod_Cat_Cart_VM()
				{
					Categories = categoryService.GetAll(),
					Products = products,
					Cart = cart,
				};

				return View(model);
			}
			else
			{
				Prod_Cat_Cart_VM model = new Prod_Cat_Cart_VM()
				{
					Categories = categoryService.GetAll(),

					Products = products,
					Cart = cartService.GetAll("CartItems").FirstOrDefault(),
				};

				return View(model);
			}
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		public IActionResult Search(string searchProdName)
		{
			List<Product> searchedProducts = productService.GetAll()
				.Where(p => p.Name.Contains(searchProdName)).ToList();

			int id = searchedProducts.Select(p => p.Id).FirstOrDefault();

			return RedirectToAction("Details", "Product", new { id });

			//Products_With_CategoriesVM products_CategoriesVM = new Products_With_CategoriesVM()
			//{
			//	Products = searchedProducts,

			//	Categories = categoryService.GetAll(),
			//};

			//ViewData["TotalPages"] = Math.Ceiling(productService.GetAll().Count() / (double)_pageSize);

			//ViewData["AllProductsNames"] = productService.GetAll().Select(c => c.Name).ToList();

			//return View("GetAll", products_CategoriesVM);
		}
	}
}
