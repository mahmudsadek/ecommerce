using Azure.Identity;
using ecommerce.Models;
using ecommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ecommerce.Controllers
{
    //[Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICartItemService cartItemService;
        private readonly IOrderItemService orderItemService;
        private readonly IProductService productService;
        private readonly IShipmentService shipmentService;

        public OrderController(IOrderService orderService,
            UserManager<ApplicationUser> userManager,
            ICartItemService cartItemService,
            IOrderItemService orderItemService,
            IProductService productService,
            IShipmentService shipmentService)
        {
            this.orderService = orderService;
            this.userManager = userManager;
            this.cartItemService = cartItemService;
            this.orderItemService = orderItemService;
            this.productService = productService;
            this.shipmentService = shipmentService;
        }

        //***********************************************

        // omar :  used GetAll Action instead
        //public IActionResult Index(string include = null)
        //{
        //    orderService.GetAll(include);

        //    return View();
        //}

        [HttpGet]
        public IActionResult GetAll(string? include = null)
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
        [Authorize(Roles = "Admin")]
        public IActionResult Insert()
        {
            /// TODO : continue from here make the VM and test the view
            /// Note : check Saeed for the register and login pages
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Order order)
        {
            orderService.Delete(order);

            orderService.Save();

            return RedirectToAction("GetAll");
        }

        //--------------------------------------------
        public async Task<IActionResult> checkout(int CartId, string UserId)
        {
            HttpContext.Session.SetString("uId", UserId);
            HttpContext.Session.SetInt32("cId", CartId);

            List<CartItem> items = cartItemService.Get(i => i.CartId == CartId);
            ApplicationUser? user = await userManager.FindByIdAsync(UserId);
            if (user != null)
            {
                Order order = new Order()
                { ApplicationUserId = user.Id, OrderDate = DateTime.Now, OrderItems = new List<OrderItem>() };
                
                foreach (var item in items)
                {
                    OrderItem orderItem = new OrderItem()
                    { ProductId = item.ProductId, OrderId = order.Id, Quantity = item.Quantity };
                    order.OrderItems.Add(orderItem);
                    //orderItemService.Insert(orderItem);
                }
                var serializedRecords = JsonSerializer.Serialize(order);
                HttpContext.Session.SetString("order", serializedRecords);
                Shipment shipment = new Shipment() { Date = DateTime.Now.AddDays(3)};
                decimal total = 0;
                foreach (OrderItem item in order.OrderItems)
                {
                    item.Product = productService.Get(item.ProductId);
                    total += item.Product.Price * item.Quantity;
                }
                ViewBag.order = order;
                ViewBag.total = total;
                return View(shipment);
            }
            return Json("Error");
        }

        [HttpPost]
        public IActionResult checkout(Shipment s)
        {
            if(ModelState.IsValid)
            {
                var order = HttpContext.Session.Get("order");
                Order orderDesrialized = JsonSerializer.Deserialize<Order>(order);
                Order o = new Order()
                {
                    OrderDate = orderDesrialized.OrderDate,
                    ApplicationUserId = orderDesrialized.ApplicationUserId,
                };
                orderService.Insert(o);
                foreach (OrderItem item in orderDesrialized.OrderItems)
                {
                    item.OrderId = o.Id;
                    Product prod = productService.Get(item.ProductId);
                    prod.Quantity -= item.Quantity;
                    orderItemService.Insert(item);
                    productService.Update(prod);
                    productService.Save();
                }
                s.OrderId = o.Id;
                s.Date = DateTime.Now.AddDays(3);
                shipmentService.Insert(s);
                o.ShipmentId = s.Id;
                orderService.Save();
                s.Order = null;
                return View("PlaceOrder");
            }
            string? userid = HttpContext.Session.GetString("uId");
            int? cartid = HttpContext.Session.GetInt32("cId");

            return RedirectToAction("checkout",new {CartId = cartid, UserId = userid});
        }
    }
}