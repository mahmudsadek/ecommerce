using ecommerce.Models;
using ecommerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService cartService;
        private readonly IProductService productService;
        private readonly ICartItemService cartItemService;

        //public static Cart SingleCart { get; private set; }

        public CartController
            (ICartService cartService ,
            IProductService productService ,
            ICartItemService cartItemService)
        {
            this.cartService = cartService;
            this.productService = productService;
            this.cartItemService = cartItemService;

            //SingleCart = new Cart()
            //{
            //    CartItems = new List<CartItem>()
            //};

        //    SingleCart = new Cart() { CartItems = new List<CartItem>()};

        //    cartService.Insert(SingleCart);

        //    cartService.Save();
        }

        //*****************************************************

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Cart> carts = cartService.GetAll();

            return View(carts);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Cart cart = cartService.Get(id);

            if (cart != null)
            {
                return View(cart);
            }

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public IActionResult Get(Func<Cart, bool> where)
        {
            List<Cart> carts = cartService.Get(where);

            return View(carts);
        }

        //--------------------------------------------

        [HttpGet]
        // [Authorize("Admin")]
        public IActionResult Insert()
        {
            /// TODO : continue from here make the VM and test the view
            /// Note : check Saeed for the register and login pages
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // [Authorize("Admin")]
        public IActionResult Insert(Cart cart)
        {
            if (ModelState.IsValid)
            {
                cartService.Insert(cart);

                cartService.Save();

                return RedirectToAction("GetAll");
            }

            return View(cart);
        }

        //--------------------------------------------

        [HttpGet]
        // [Authorize("Admin")]
        public IActionResult Update(int id)
        {
            Cart cart = cartService.Get(id);

            if (cart != null)
            {
                return View(cart);
            }

            return RedirectToAction("GetAll");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //   [Authorize("Admin")]
        public IActionResult Update(Cart cart)
        {
            if (ModelState.IsValid)
            {
                cartService.Update(cart);

                cartService.Save();

                return RedirectToAction("GetAll");
            }

            return View(cart);
        }

        //--------------------------------------------

        [HttpGet]
        // [Authorize("Admin")]
        public IActionResult Delete(int id)
        {
            Cart cart = cartService.Get(id);

            if (cart != null)
            {
                return View(cart);
            }

            return RedirectToAction("GetAll");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // [Authorize("Admin")]
        public IActionResult Delete(Cart cart)
        {
            cartService.Delete(cart);

            cartService.Save();

            return RedirectToAction("GetAll");
        }

        //********************************************************

        //public void AddToCart(int id, int quantity)
        //{
        //    Product product = productService.Get(id);

        //    CartItem item = new CartItem()
        //    {
        //        Quantity = quantity,

        //        ProductId = id,
        //        Product = product,

        //        CartId = SingleCart.Id ,
        //        Cart = SingleCart, 
        //    };

        //    cartItemService.Insert(item);

        //    cartItemService.Save();

        //    cartService.Save();

        //}
    }
}
