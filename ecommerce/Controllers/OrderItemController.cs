using ecommerce.Models;
using ecommerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Controllers
{
    public class OrderItemController : Controller
    {
        private readonly IOrderItemService orderItemService;

        public OrderItemController(IOrderItemService orderItemService)
        {
            this.orderItemService = orderItemService;
        }
        [HttpGet]
        public IActionResult GetAll(string? include = null)
        {
            List<OrderItem> orderItems = orderItemService.GetAll(include);
            return View(orderItems);
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            OrderItem orderItem = orderItemService.Get(id);

            if (orderItem != null)
            {
                return View(orderItem);
            }

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public IActionResult Get(Func<Order, bool> where)
        {
            List<OrderItem> orderItems = orderItemService.Get(where);

            return View(orderItems);
        }

        [HttpGet]
        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Insert(OrderItem orderItem)
        {
            if (ModelState.IsValid)
            {
                orderItemService.Insert(orderItem);

                orderItemService.Save();

                return RedirectToAction("GetAll");
            }

            return View(orderItem);
        }

        [HttpPost]
        public IActionResult Update(OrderItem orderItem)
        {
            if (ModelState.IsValid)
            {
                orderItemService.Update(orderItem);

                orderItemService.Save();

                return RedirectToAction("GetAll");
            }

            return View(orderItem);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            OrderItem orderItem = orderItemService.Get(id);

            if (orderItem != null)
            {
                return View(orderItem);
            }

            return RedirectToAction("GetAll");
        }

        [HttpPost]
        public IActionResult Delete(OrderItem orderItem)
        {
            orderItemService.Delete(orderItem);

            orderItemService.Save();

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            OrderItem orderItem = orderItemService.Get(id);

            if (orderItem != null)
            {
                return View(orderItem);
            }

            return RedirectToAction("GetAll");
        }
    }
}
