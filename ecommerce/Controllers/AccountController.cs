using ecommerce.Models;
using ecommerce.Services;
using ecommerce.ViewModel;
using ecommerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Security.Claims;
using System.Text;

namespace ecommerce.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<ApplicationUser> _userManager ,
            SignInManager<ApplicationUser> _signInManager , RoleManager<IdentityRole> _roleManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            roleManager = _roleManager;
            
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult register(bool IsAdmin = false)
        {
            ViewBag.IsAdmin = IsAdmin;
            return View("register");
        }


        [HttpPost]
        public async Task<IActionResult> register(RegisterViewModel model , bool IsAdmin)
        {
            ApplicationUser applicationUser = new ApplicationUser 
            {
                UserName = model.userName,
                PasswordHash = model.password,
                PhoneNumber = model.phoneNumber,
/**/                Email = model.Email?.Trim()
            };

            if(ModelState.IsValid)
            {
                IdentityResult result = new IdentityResult();
                try
                {
                   result = await userManager.CreateAsync(applicationUser,
                   applicationUser.PasswordHash);
                }
                catch(Exception ex) 
                {
                   ModelState.AddModelError(string.Empty, "Already existing email");
                }
                
               // IsAdmin = true; 
                if(result.Succeeded)
                {
                    switch(IsAdmin)
                    {
                        case true:
                            await userManager.AddToRoleAsync(applicationUser, "Admin");
                            break;
                        default:
                            await userManager.AddToRoleAsync(applicationUser, "User");
                            break;
                    }
                    return View("login");
                }
              
          
                foreach(IdentityError err in result.Errors) 
                {
                    ModelState.AddModelError(string.Empty , err.Description); 
                }

            }

            ViewBag.IsAdmin = IsAdmin;
            return View("register");

        }

        [HttpGet]
        public IActionResult login()
        {
            return View("login");
        } 

        // omar : saeed take a look at what happens when the user enters a wrong passwprd at login
        [HttpPost , ValidateAntiForgeryToken]
        public async Task <IActionResult> login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser user = await userManager.FindByNameAsync(model.userName);
                if(user != null)
                {
                 bool matched = await userManager.CheckPasswordAsync(user, model.password);
                    if(matched) 
                    {
                    await signInManager.SignInAsync(user, model.rememberMe); 
                      return RedirectToAction("Index", "Home");  
                    }
                    ModelState.AddModelError("", "invalid password");
                    return View("login");
                }
            }
            ModelState.AddModelError("", "invalid user name");
            return View("login" , model); 
        }


        public async Task<IActionResult> logout()
        {
            await signInManager.SignOutAsync();   
            return RedirectToAction("login"); 
        }


        [HttpGet , Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAdmin()
        {
            List<string> userNames = new List<string>();

            IList<ApplicationUser> users =  await userManager.GetUsersInRoleAsync("User");

            foreach (ApplicationUser user in users)
            {
                userNames.Add(user.UserName);
            }

            return View(userNames);
        }


        public async Task<IActionResult> confirmMakeAdmin(string userName)
        {
            ApplicationUser user = await userManager.FindByNameAsync(userName);
            if (user != null)
            {
                await userManager.RemoveFromRoleAsync(user, "User");
                await userManager.AddToRoleAsync(user, "Admin");
                return RedirectToAction("addadmin");
            }
            return RedirectToAction("addadmin");
        }

        public async Task <IActionResult> removeAdmin(string userName)
        {
            List<string> userNames = new List<string>();

            IList<ApplicationUser> users = await userManager.GetUsersInRoleAsync("Admin");

            foreach (ApplicationUser user in users)
            {  
                userNames.Add(user.UserName);
            }

            return View(userNames);
        }

        public async Task<IActionResult> confirmRemoveAdmin(string userName)
        {
            ApplicationUser user = await userManager.FindByNameAsync(userName);
            if (user != null) 
            {
                await userManager.RemoveFromRoleAsync(user, "Admin");
                await userManager.AddToRoleAsync(user, "User");
                return RedirectToAction("removeAdmin");
            }
            return RedirectToAction("removeAdmin");
        }


        [HttpGet]
        public IActionResult forgotPassword() 
        {
            return View("forgotPassword");
        }


        [HttpPost]
        public async Task <IActionResult> forgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid) 
            {
                model.Email = model?.Email.Trim();
              ApplicationUser? user = await userManager.FindByEmailAsync(model.Email);

                if (user != null) 
                {
                    string token = await userManager.GeneratePasswordResetTokenAsync(user);
                  //  token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));         // saeed : search for decoding 
                    string callBackUrl = Url.Action("resetPassword", "account" , values: new { token , userName = user.UserName },
                        protocol: Request.Scheme);   
                      
                  return RedirectToAction("SendMail", "Mail", 
                      routeValues: new{ emailTo = user.Email , username = user.UserName , callBackUrl = callBackUrl });   
                   
                }
                ModelState.AddModelError("", "Email not existed");
                return View("forgotPassword", model);  
            }
            return View("forgotPassword", model);
        }


        [HttpGet]
        public IActionResult ResetPassword([FromQueryAttribute]string userName , [FromQueryAttribute]string token)  
        {
            ViewBag.UserName = userName;
            ViewBag.Token = token;
            return View("resetPassword");
        }

        [HttpPost]
        public async Task <IActionResult> ResetPassword(resetPasswordViewModel model)
        {
            if (ModelState.IsValid) 
            {
              ApplicationUser? user = await userManager.FindByNameAsync(model.userName);
             IdentityResult result = await userManager
                    .ResetPasswordAsync(user, model.token, model.newPassword);


                if (result.Succeeded)
                return View("login");

                else
                {
                    foreach (IdentityError error in result.Errors)
                    { ModelState.AddModelError(string.Empty, error.Description); }

                    return View("resetPassword", model);
                }
            } 
            return View("resetPassword" , model);
        }
    }
}
