using ecommerce.Models;
using ecommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Controllers
{
    //[Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }
        // hello abdo 

        // omar :  used GetAll Action instead
        //public IActionResult Index(string include = null)
        //{
        //    orderService.GetAll(include);

        //    return View();
        //}

        [HttpGet]
        public IActionResult GetAll(string include = null)
        {
            List<Order> orders = orderService.GetAll(include);

            return View(orders);
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            Order order = orderService.Get(id);

            if (order != null)
            {
                return View(order);
            }

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public IActionResult Get(Func<Order, bool> where)
        {
            List<Order> orders = orderService.Get(where);

            return View(orders);
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
        public IActionResult Insert(Order order)
        {
            if (ModelState.IsValid)
            {
                orderService.Insert(order);

                orderService.Save();

                return RedirectToAction("GetAll");
            }

            return View(order);
        }

        //--------------------------------------------

        [HttpGet]
        // [Authorize("Admin")]
        public IActionResult Update(int id)
        {
            Order order = orderService.Get(id);

            if (order != null)
            {
                return View(order);
            }

            return RedirectToAction("GetAll");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //   [Authorize("Admin")]
        public IActionResult Update(Order order)
        {
            if (ModelState.IsValid)
            {
                orderService.Update(order);

                orderService.Save();

                return RedirectToAction("GetAll");
            }

            return View(order);
        }

        //--------------------------------------------

        [HttpGet]
        // [Authorize("Admin")]
        public IActionResult Delete(int id)
        {
            Order order = orderService.Get(id);

            if (order != null)
            {
                return View(order);
            }

            return RedirectToAction("GetAll");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // [Authorize("Admin")]
        public IActionResult Delete(Order order)
        {
            orderService.Delete(order);

            orderService.Save();

            return RedirectToAction("GetAll");
        }

        //--------------------------------------------

    }
}