using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Controllers
{
    public class DashbourdController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult products()
        {
            return View(); 
        }
        public IActionResult orders()
        {
            return View();
        }
        public IActionResult categories()
        {
            return View();
        }

    }
}
