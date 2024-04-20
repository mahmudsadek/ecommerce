using ecommerce.Models;
using ecommerce.Repository;
using ecommerce.Services;
using ecommerce.ViewModel;
using ecommerce.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using System.Drawing.Printing;
using System.Security.Claims;

namespace ecommerce.Controllers
{
	public class ProductController : Controller
	{
		private readonly IProductService productService;
        private readonly ICartService cartService;
        private readonly ICartItemService cartItemService;
        private readonly ICommentService commentService;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public ICategoryService categoryService { get; }

		private const int _pageSize = 6;

		public ProductController
			(IProductService productService, ICategoryService categoryService ,
			ICartService cartService , ICartItemService cartItemService,ICommentService commentService
			, IWebHostEnvironment webHostEnvironment)
		{
			this.commentService = commentService;
			this.productService = productService;

			this.categoryService = categoryService;
            this.cartService = cartService;
            this.cartItemService = cartItemService;
			this._webHostEnvironment = webHostEnvironment;
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

            ViewBag.PageSize = pageSize;

            ViewBag.TotalProductsNumber = productService.GetAll().Count();

            List<Cart> carts = cartService.GetAll();

            // Get the user ID
            string? userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ViewBag.UserId = userIdClaim;

            if (carts.Count == 0)
			{
				Cart cart = new Cart() { CartItems = new List<CartItem>()};

                Products_With_CategoriesVM products_CategoriesVM1 = new Products_With_CategoriesVM()
                {
                    Products = PaginatedProducts,
                    Categories = categoryService.GetAll(),
                    Cart = cart
                };

                return View(products_CategoriesVM1);
			}
			else
			{
                Products_With_CategoriesVM products_CategoriesVM2 = new Products_With_CategoriesVM()
                {
                    Products = PaginatedProducts,
                    Categories = categoryService.GetAll(),
                    Cart = cartService.GetAll("CartItems").FirstOrDefault(),
                };

                return View(products_CategoriesVM2);
            }
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

			int id = searchedProducts.Select(p => p.Id).FirstOrDefault();

			return RedirectToAction("Details" , "Product" , new { id =	id });

        }

        [HttpGet]
		public IActionResult Details(int id)
		{
			Product productDB = productService.Get(id);
			
            ViewBag.Comments = commentService.GetComments(c=>c.ProductId == id);

            if (productDB != null)
			{
				Category prodCateg = categoryService.Get(productDB.CategoryId);

				Cart? cart = cartService?.GetAll("CartItems")?.FirstOrDefault();

                // Get the user ID
                string userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

                ViewBag.UserId = userIdClaim;

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

					RealtedProducts = productService.Get(p => p.CategoryId == productDB.CategoryId).Take(3).ToList() ,

					Cart = cart,

					Categories = categoryService.GetAll(),
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
        [Authorize(Roles = "Admin")]
        public IActionResult Insert()
		{
			ProductWithListOfCatesViewModel product = new();
			product.categories = categoryService.GetAll();
			return View(product);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Insert(ProductWithListOfCatesViewModel product)
		{
            string uploadpath = Path.Combine(_webHostEnvironment.WebRootPath, "img");
            string imagename = Guid.NewGuid().ToString() + "_" + product.image.FileName;
            string filepath = Path.Combine(uploadpath, imagename);
            using (FileStream fileStream = new FileStream(filepath, FileMode.Create))
            {
                product.image.CopyTo(fileStream);
            }
            product.ImageUrl = imagename; 

            if (ModelState.IsValid)
			{

				productService.Insert(product);

				return RedirectToAction("products","Dashbourd");
			}

			return View(product);
		}

		//--------------------------------------------

		[HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id)
		{
            ProductWithListOfCatesViewModel product = productService.GetViewModel(id);

			if (product != null)
			{
				return View(product);
			}

			return RedirectToAction("products", "Dashbourd");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(ProductWithListOfCatesViewModel product)
		{
            string uploadpath = Path.Combine(_webHostEnvironment.WebRootPath, "img");
            string imagename = Guid.NewGuid().ToString() + "_" + product.image.FileName;
            string filepath = Path.Combine(uploadpath, imagename);
            using (FileStream fileStream = new FileStream(filepath, FileMode.Create))
            {
                product.image.CopyTo(fileStream);
            }
            product.ImageUrl = imagename;


            if (ModelState.IsValid)
			{
				productService.Update(product);

				productService.Save();

				return RedirectToAction("products", "Dashbourd");
			}

			return View(product);
		}

		//--------------------------------------------

		[HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
		{
			Product product = productService.Get(id);

			if (product != null)
			{
				return View(product);
			}

			return RedirectToAction("products", "Dashbourd");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public IActionResult Delete(Product product)
		{
			productService.Delete(product);

			productService.Save();

			return RedirectToAction("products", "Dashbourd");
		}

		//*******************************************************

		public IActionResult AddtoCart(int id , int quantity = 1)
		{
			List<Cart> carts = cartService.GetAll("CartItems");

            Product product = productService.Get(id);

            if (carts.Count == 0) // the first product added to cart => then create an insatace from cart
			{
                Cart cart = new Cart() { CartItems = new List<CartItem>() };

                cartService.Insert(cart);

                cartService.Save();

                //-------------------

                CartItem cartItem = new CartItem()
                {
                    Quantity = quantity,

                    ProductId = id,
                    Product = product,

                    CartId = cart.Id,
                    Cart = cart,
                };

                cartItemService.Insert(cartItem);

                cartItemService.Save();

                cartService.Save();

                return RedirectToAction("Details", "Product", new { id = id });

            }
            else
			{
				Cart cart = cartService.GetAll("CartItems").FirstOrDefault();

				CartItem? existedItem = cart.CartItems.FirstOrDefault(c => c.ProductId == id);

				if (existedItem != null)
				{
					existedItem.Quantity += quantity;
                }
				else
				{
                    CartItem cartItem = new CartItem()
                    {
                        Quantity = quantity,

                        ProductId = id,
                        Product = product,

                        CartId = cart.Id,
                        Cart = cart,
                    };

                    cartItemService.Insert(cartItem);

                }

                cartItemService.Save();

                cartService.Save();

                return RedirectToAction("Details", "Product", new { id = id });

            }
        }






      
        }
    }

