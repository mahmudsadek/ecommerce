using ecommerce.Models;
using ecommerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Controllers
{
    public class ShipmentController : Controller
    {

        private IShipmentService shipmentService;

        public ShipmentController(IShipmentService shipment)
        {
            shipmentService = shipment;
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
        
    }
}
