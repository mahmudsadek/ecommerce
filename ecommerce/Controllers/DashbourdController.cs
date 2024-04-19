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
        private readonly IOrderItemService orderItemService;

        public DashbourdController(IProductService productService, 
            ICategoryService categoryService, 
            ICommentService commentService,
            IOrderService orderService,
            UserManager<ApplicationUser> userManager,
            IOrderItemService orderItemService
            )
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.commentService = commentService;
            this.orderService = orderService;
            this.userManager = userManager;
            this.orderItemService = orderItemService;
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

        public IActionResult OrderDetails(int id)
        {
            decimal total = 0;
            Order order = orderService.Get(id);
            order.OrderItems = orderItemService.Get(i => i.OrderId == id);
            foreach (OrderItem item in order.OrderItems)
            {
                item.Product = productService.Get(item.ProductId);
                total += item.Product.Price * item.Quantity;
            }
            ViewBag.total = total;
            return View(order);
        }

        public IActionResult categoryDetails(int id)
        {
            Category cate = categoryService.Get(id);
            return View(cate);
        }

        public IActionResult getOrdersPartial()
        {
            List<Order> orders = orderService.GetAll("User");

            return PartialView("_getOrdersPartial",orders);
        }
        public IActionResult categories()
        {
            List<Category> cates = categoryService.GetAll();
            return View(cates);
        }

        public async Task <IActionResult> users()
        {
            IList<ApplicationUser> users = await userManager.GetUsersInRoleAsync("User"); 
            return View(users);
        }

        public IActionResult numberOfUsers()
        {
            int users =  userManager.Users.Count();
            return Json(users);
        }


        public async Task<IActionResult> admins()
        {
            IList<ApplicationUser> users = await userManager.GetUsersInRoleAsync("Admin");
            return View(users);
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

        public IActionResult GetTotalOrdersPrice()
        {
            decimal total = 0;
            List<Order> orders = orderService.GetAll("OrderItems");
            
            foreach(Order order in orders)
            {
                foreach (OrderItem item in order.OrderItems)
                {
                    item.Product = productService.Get(item.ProductId);
                    total += item.Product.Price * item.Quantity;
                }
            }
            return Json(total);
        }

        public IActionResult OrderCount()
        {
            List<Order> orders = orderService.GetAll("OrderItems");
            return Json(orders.Count);
        }


        // saeed
        public async Task <IActionResult> deleteAccount(string userName) 
        {
          ApplicationUser? userApp = await userManager.FindByNameAsync(userName);
            if (userApp != null) 
            {
              if(await userManager.IsInRoleAsync(userApp, "Admin")) 
                {
                  await userManager.RemoveFromRolesAsync(userApp, await userManager.GetRolesAsync(userApp)); 
                  await userManager.DeleteAsync(userApp);
                    if(User.FindFirst("name")?.Value == userName)
                    {
                        return RedirectToAction("logout" , "account"); 
                    }
                    return RedirectToAction("admins");  
                }

              else
                {
                    await userManager.RemoveFromRolesAsync(userApp, await userManager.GetRolesAsync(userApp));
                    await userManager.DeleteAsync(userApp);
                    return RedirectToAction("users");
                }
                    
            }
            return RedirectToAction("users");

        }

    }
}
