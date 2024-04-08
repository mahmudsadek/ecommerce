using ecommerce.Models;
using ecommerce.Services;
using ecommerce.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
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

        public DashbourdController(IProductService productService, 
            ICategoryService categoryService, 
            ICommentService commentService,
            IOrderService orderService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.commentService = commentService;
            this.orderService = orderService;
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
        public IActionResult categories()
        {
            return View();
        }

    }
}
