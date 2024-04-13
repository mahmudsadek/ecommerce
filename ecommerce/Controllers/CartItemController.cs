using ecommerce.Models;
using ecommerce.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ecommerce.Controllers
{
    public class CartItemController : Controller
    {
        private readonly ICartItemService cartItemService;
        private readonly ICartService cartService;

        public CartItemController(ICartItemService cartItemService , ICartService cartService)
        {
            this.cartItemService = cartItemService;
            this.cartService = cartService;
        }

        //*************************************************

        [HttpGet]
        public IActionResult GetAll()
        {
            List<CartItem> cartItems = cartItemService.GetAll("Product");

            //ViewBag.Cart = cartService.GetAll("CartItems").FirstOrDefault();

            // Get the user ID
            string userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ViewBag.UserId = userIdClaim;

            return View(cartItems);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            CartItem cartItem = cartItemService.Get(id);

            if (cartItem != null)
            {
                return View("Get" ,cartItem);
            }

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public IActionResult Get(Func<CartItem, bool> where)
        {
            List<CartItem> cartItems = cartItemService.Get(where);

            return View(cartItems);
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
        public IActionResult Insert(CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                cartItemService.Insert(cartItem);

                cartItemService.Save();

                return RedirectToAction("GetAll");
            }

            return View(cartItem);
        }

        //--------------------------------------------

        [HttpGet]
        // [Authorize("Admin")]
        public IActionResult Update(int id)
        {
            CartItem cartItem = cartItemService.Get(id);

            if (cartItem != null)
            {
                return View(cartItem);
            }

            return RedirectToAction("GetAll");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //   [Authorize("Admin")]
        public IActionResult Update(CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                CartItem item = cartItemService.GetAll("Product").FirstOrDefault(c => c.Id == cartItem.Id);

                item.Quantity = cartItem.Quantity;

                cartItemService.Update(cartItem);

                cartItemService.Save();

                cartService.Save();

                return RedirectToAction("GetAll");
            }

            return View(cartItem);
        }

        //--------------------------------------------

        //[HttpGet]
        //// [Authorize("Admin")]
        //public IActionResult Delete(int id)
        //{
        //    CartItem cartItem = cartItemService.Get(id);

        //    if (cartItem != null)
        //    {
        //        return View(cartItem);
        //    } 

        //    return RedirectToAction("GetAll");
        //}


        //[ValidateAntiForgeryToken]   Omar : don't turn it on because it consider the js snip message code as a differnet domain => debug later if there is time .
        // [Authorize("Admin")]
        public IActionResult Delete(int id)
        {
            CartItem cartItem = cartItemService.Get(id);

            cartItemService.Delete(cartItem);

            cartItemService.Save();

            return RedirectToAction("GetAll" , "CartItem");
        }

        //********************************************************

    }
}
