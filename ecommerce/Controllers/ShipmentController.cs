using ecommerce.Models;
using ecommerce.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;

namespace ecommerce.Controllers
{
    public class ShipmentController : Controller
    {

        private IShipmentService shipmentService;
		private readonly IOrderItemService orderItemService;
		private readonly IOrderService orderService;

		public ShipmentController(IShipmentService shipment, IOrderItemService orderItemService , IOrderService orderService)
        {
            shipmentService = shipment;
			this.orderItemService = orderItemService;
			this.orderService = orderService;
		}


        [HttpGet]
        public IActionResult GetAll(string? include = null)
        {
            List<Shipment> shipmentList = shipmentService.GetAll(include);

            return View("GetAll", shipmentList);
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            Shipment shipment = shipmentService.Get(id);

            if (shipment != null)
            {
                return View(shipment);
            }
            
            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public IActionResult Get(Func<Shipment, bool> where)
        {
            List<Shipment> shipments = shipmentService.Get(where);

            return View(/*"GetAll",*/shipments);
        }

        [HttpGet]
        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //   [Authorize("Admin")]
        public IActionResult Insert(Shipment shipment)
        {
            if (ModelState.IsValid)
            {
                shipmentService.Insert(shipment);

                shipmentService.Save();

                return RedirectToAction("GetAll");
            }

            return View(shipment);
        }

        [HttpGet]
        // [Authorize("Admin")]
        public IActionResult Update(int id)
        {
            Shipment shipment = shipmentService.Get(id);

            if (shipment != null)
            {
                return View(shipment);
            }

            return RedirectToAction("GetAll");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //   [Authorize("Admin")]
        public IActionResult Update(Shipment shipment)
        {
            if (ModelState.IsValid)
            {
                shipmentService.Update(shipment);

                shipmentService.Save();

                return RedirectToAction("GetAll");
            }

            return View(shipment);
        }

        [HttpGet]
        // [Authorize("Admin")]
        public IActionResult Delete(int id)
        {
            Shipment shipment = shipmentService.Get(id);

            if (shipment != null)
            {
                return View(shipment);
            }

            return RedirectToAction("GetAll");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // [Authorize("Admin")]
        public IActionResult Delete(Shipment shipment)
        {
            shipmentService.Delete(shipment);

            shipmentService.Save();

            return RedirectToAction("GetAll");
        }

        public IActionResult PlaceShipment(Shipment s)
        {
            
                var order = HttpContext.Session.Get("order");
                Order orderDesrialized = JsonSerializer.Deserialize<Order>(order);
                Order o = new Order() { 
                    OrderDate = orderDesrialized.OrderDate , 
                    ApplicationUserId = orderDesrialized.ApplicationUserId , 
                    };
                orderService.Insert(o);
                s.OrderId = o.Id;
                s.Date = DateTime.Now.AddDays(3);
                shipmentService.Insert(s);
                o.ShipmentId = s.Id;
                orderService.Save();
                s.Order = null;
                return View(s);
            
        }

    }
}
