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

        //saeed : add roles when needed instead of in model creating

        // omar : any one but saeed should call this func at least twice to create the Admin role and User role

        public async Task<IActionResult> addRole()
        {
            IdentityRole role = new IdentityRole();


          //  role.Name = "Admin";    // first call 

          //  role.Name = "User";   // second call 

            await roleManager.CreateAsync(role);

            return RedirectToAction("Register" , "Account");
        }

    }
}
