using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ecommerce.Controllers
{
    public class RoleController : Controller
    {
         
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> _roleManager)
        {
            roleManager = _roleManager;
        }



        // saeed : add roles when needed instead of in model creating
        //public async Task<IActionResult> addRole()
        //{
        //    IdentityRole role = new IdentityRole();
        //    role.Name = "Admin";
        //    await roleManager.CreateAsync(role);
        //    return View("register", "account");
        //}

    }
}
