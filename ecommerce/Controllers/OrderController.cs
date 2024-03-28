using ecommerce.Models;
using ecommerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService orderService;

        public OrderController(OrderService orderService)
        {
            this.orderService = orderService;
        }

        public IActionResult Index(string include = null)
        {
            orderService.GetAll(include);

            return View();
        }

        public IActionResult GetAll(string include = null)
        {
            orderService.GetAll(include);

            return View();
        }

        public IActionResult Get(int id)
        {
            orderService.Get(id);

            return View();
        }

        public IActionResult Get(Func<Order, bool> where)
        {
            orderService.Get(where);

            return View();
        }

        //--------------------------------------------

        [HttpGet]
        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Insert(Order order)
        {
            if (ModelState.IsValid)
            {
                orderService.Insert(order);

                return RedirectToAction("Index");
            }

            return View(order);
        }

        //--------------------------------------------

        [HttpGet]
        public IActionResult Update(int id)
        {
           Order order = orderService.Get(id);

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Order order)
        {
            if (ModelState.IsValid)
            {
                orderService.Update(order);

                orderService.Save();

                return RedirectToAction("Index");
            }

            return View();
        }

        //--------------------------------------------

        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Order order)
        {
            orderService.Delete(order);

            return View();
        }

        //--------------------------------------------

    }
}