using ecommerce.Models;
using ecommerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService cartService;

        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        //*******************************************

        // omar :  used GetAll Action instead
        //public IActionResult Index(string include = null)
        //{
        //    orderService.GetAll(include);

        //    return View();
        //}

        [HttpGet]
        public IActionResult GetAll(string? include = null)
        {
            List<Cart> carts = cartService.GetAll(include);

            return View(carts);
        }

        [HttpGet]
        public IActionResult Get(int id)
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

        //--------------------------------------------

    }
}
