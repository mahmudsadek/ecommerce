using ecommerce.Models;
using ecommerce.Services;
using ecommerce.ViewModels.Comments;
using ecommerce.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashbourdController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly ICommentService commentService;
        private readonly IOrderService orderService;
        private readonly UserManager<ApplicationUser> userManager;

        public DashbourdController(IProductService productService, 
            ICategoryService categoryService, 
            ICommentService commentService,
            IOrderService orderService,
            UserManager<ApplicationUser> userManager)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.commentService = commentService;
            this.orderService = orderService;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult products()
        {
            List<Product> products = productService.GetAll();
            return View(products); 
        }

        [HttpGet]
        public async Task<IActionResult> ProdDetails(int id)
        {

            ProductWithCatNameAndComments prod = await productService.WithCatNameAndComments(id);
            prod.Comments = await commentService.GetCommentWithUserName(id);
            //Product product = productService.Get(id);
            //product.Category = categoryService.Get(product.CategoryId);
            //product.Comments = commentService.GetComments(c => c.ProductId == id);
            return View("ProductDetails",prod);
        }
        public IActionResult orders()
        {
            List<Order> orders =  orderService.GetAll("User");

            return View(orders);
        }

        public IActionResult getOrdersPartial()
        {
            List<Order> orders = orderService.GetAll("User");

            return PartialView("_getOrdersPartial",orders);
        }
        public IActionResult categories()
        {
            return View();
        }

        public IActionResult users()
        {
            IQueryable<ApplicationUser> users =  userManager.Users;
            return View(users);
        }

        public IActionResult numberOfUsers()
        {
            int users =  userManager.Users.Count();
            return Json(users);
        }

        public IActionResult numOfProducts()
        {
            int prods = productService.GetAll().Count();
            return Json(prods);
        }

        public async Task<IActionResult> CommentPartial()
        {
            List<CommentWithUserNameViewModel> comments =  await commentService.GetCommentWithUserNameTake(5);
            return PartialView("_CommentPartial",comments);    
        }

    }
}
