using ecommerce.Models;
using ecommerce.Repository;
using ecommerce.Services;
using ecommerce.ViewModel;
using ecommerce.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
        private readonly IRepository<Shipment> shipmentRepository;    // saeed : edit it
        private readonly IOrderItemRepository orderItemRepository;
        private readonly IProductRepository productRepository;

        public AccountController(UserManager<ApplicationUser> _userManager ,
            SignInManager<ApplicationUser> _signInManager , RoleManager<IdentityRole> _roleManager,
            IRepository<Shipment> _Repository, IOrderItemRepository _orderItemRepository,
            IProductRepository _productRepository)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            roleManager = _roleManager;
            shipmentRepository = _Repository;
            orderItemRepository = _orderItemRepository;
            productRepository = _productRepository;
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


        [HttpGet]
        public async Task <IActionResult> mailConfirmed(string email)
        {
            ApplicationUser? user = await userManager.FindByEmailAsync(email);
            user.EmailConfirmed = true;
            await userManager.UpdateAsync(user);
            return RedirectToAction("login"); 
        }


        [HttpPost]    
        public async Task<IActionResult> register(RegisterViewModel model, bool isAdmin)  
        {
            ApplicationUser applicationUser = new ApplicationUser
            {
                UserName = model.userName,
                PasswordHash =model.password,
                PhoneNumber = model.phoneNumber,
                Email = model.Email?.Trim()
            };

            if (ModelState.IsValid)                 
            {
                IdentityResult result = new IdentityResult();
                try
                {
                    result = await userManager.CreateAsync(applicationUser,
                    applicationUser.PasswordHash);
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.StartsWith("Cannot insert duplicate key"))  // if modelstate is valid >> no thing can make excption except duplicate email >> this line for more verification
                    ModelState.AddModelError(string.Empty, "Already existing email");  // saeed : may cause bugs

                    else { ModelState.AddModelError(string.Empty, ex.InnerException.Message); }
                }

               // isAdmin = true; 
                if (result.Succeeded)
                {
                    switch (isAdmin)
                    {
                        case true:
                            await userManager.AddToRoleAsync(applicationUser, "Admin");
                            return RedirectToAction("admins", "dashbourd");
                            break;

                        default:
                            await userManager.AddToRoleAsync(applicationUser, "User");
                            if (User.IsInRole("Admin"))
                                return RedirectToAction("users", "dashbourd");
                            break;
                    }
                    //return View("login"); 
                    return RedirectToAction("SendForceEmailConfirmationMail" , "Mail" , new { toEmail = model.Email});  // email sent null!!!!!!!!!!
                }


                foreach (IdentityError err in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, err.Description);
                }

            }

            ViewBag.IsAdmin = isAdmin;
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
                        if(user.EmailConfirmed)
                        {
                           List<Claim> claims = new List<Claim>();
                            claims.Add(new Claim("name", model.userName));
                            await signInManager.SignInWithClaimsAsync(user, model.rememberMe, claims);
                            return RedirectToAction("Index", "Home");
                        }
                        ModelState.AddModelError("", "Unconfirmed email");
                        return View("login");
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


        // hoda aswan made another func in dashboard

        //[HttpGet , Authorize(Roles = "Admin")]
        //public async Task<IActionResult> AddAdmin()
        //{
        //    List<string> userNames = new List<string>();

        //    IList<ApplicationUser> users =  await userManager.GetUsersInRoleAsync("User");

        //    foreach (ApplicationUser user in users)
        //    {
        //        userNames.Add(user.UserName);
        //    }

        //    return View(userNames);  
        //}


        public async Task<IActionResult> confirmMakeAdmin(string userName)
        {
            ApplicationUser user = await userManager.FindByNameAsync(userName);
            if (user != null)
            {
                await userManager.RemoveFromRoleAsync(user, "User"); 
                await userManager.AddToRoleAsync(user, "Admin");
                return RedirectToAction("users", "Dashbourd");
            }
            return RedirectToAction("users", "Dashbourd");
        }


        // hoda aswan made another func in dashboard

        //public async Task <IActionResult> removeAdmin(string userName)
        //{
        //    List<string> userNames = new List<string>();

        //    IList<ApplicationUser> users = await userManager.GetUsersInRoleAsync("Admin");

        //    foreach (ApplicationUser user in users)
        //    {  
        //        userNames.Add(user.UserName);
        //    }

        //    return View(userNames);
        //}



        public async Task<IActionResult> confirmRemoveAdmin(string userName)
        {
            ApplicationUser appUser = await userManager.FindByNameAsync(userName);
            if (appUser != null) 
            {
                await userManager.RemoveFromRoleAsync(appUser, "Admin");
                await userManager.AddToRoleAsync(appUser, "User");
                if(User.FindFirst("name")?.Value == userName)
                {
                    return RedirectToAction("logout");
                }
                return RedirectToAction("admins" , "dashbourd"); 
            } 
            return RedirectToAction("admins", "dashbourd");   // which view should be returned if user not found!!!!!
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




        public async Task <IActionResult> myAccount(string selectedPartial = "_accountInfoPartial", resetPasswordViewModel changePasswordModel = null)
        {
			//Claim nameClaim = User.Claims.FirstOrDefault(c => c.Type == "name");
			//if (nameClaim != null)
			//    ViewBag.Name = nameClaim.Value; 
			ApplicationUser user = await userManager.FindByNameAsync(User.Identity.Name);  // err
			AccountInfoViewModel model = new AccountInfoViewModel()
			{
				userName = user.UserName,
				phoneNumber = user.PhoneNumber,
				Email = user.Email
			};
            ViewBag.selectedPartial = selectedPartial;
            ViewBag.resetPasswordModel = changePasswordModel;
			return View(model);
        }



        public async Task <IActionResult> getAccountInfoPartial()
        {
            ApplicationUser user = await userManager.FindByNameAsync (User.Identity.Name);
            AccountInfoViewModel model = new AccountInfoViewModel() { userName = user.UserName,
                phoneNumber = user.PhoneNumber, Email = user.Email };
            return View("_accountInfoPartial" , model);  // send v.m 
        }

        public IActionResult getAccountChangePasswordPartial()
        {
            return View("_accountChangePasswordPartial");
        }


        public IActionResult getAccountOrdersPartial()
        {
            return View("_accountOrdersPartial");
        }


        public async Task <IActionResult> getAccountShipmentsPartial()
        {
            List<string> randomProductImages = new List<string>();
          //  return Content("sd");
           ApplicationUser? user = await userManager.FindByNameAsync(User.Identity.Name);
            List<Shipment>? shipments = shipmentRepository.Get(s => s.UserId == user.Id);

            //if (shipments.Count == 0)
            //    return View("NotFound"); 
            return View("_accountShipmentsPartial" , shipments);
        }


      


        [HttpPost]
        public async Task <IActionResult> editAccountInfo(string userName , string phoneNumber , string Email)  // cannot send model it self from partial view , when try serialize and send json from view >> json come with old data
        {
            AccountInfoViewModel model = new AccountInfoViewModel()
            {
                userName = userName,
                phoneNumber = phoneNumber,
                Email = Email
            };

            if(ModelState.IsValid)
            {
                ApplicationUser user = await userManager.FindByEmailAsync(Email);
                if (user != null)
                {
                    try
                    {
                        user.UserName = model.userName;
                        user.PhoneNumber = model.phoneNumber;
                        await userManager.UpdateAsync(user);      // saeed : if new userName is not unique to validation msg appear to user and no update also will happen try to solve
                    }
                    catch(Exception ex) 
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                        return RedirectToAction("myAccount", "account", model);
                    }


                    //ClaimsIdentity claimsIdentity = new ClaimsIdentity(User.Identity);
                    //claimsIdentity.RemoveClaim(claimsIdentity.FindFirst(ClaimTypes.Name));
                    //claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, model.userName));

                    //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    if(User.Identity.Name != user.UserName)
                        return RedirectToAction("logout", "account");   // saeed : try to update name in claim without log user out

                    return RedirectToAction("Index" , "Home"); 
                }
                ModelState.AddModelError("invalidSentEmail", "User not fount");
                return RedirectToAction("myAccount", "account" , model);  
            }
            return RedirectToAction("myAccount", "account", model);
        }


        public async Task <IActionResult> editAccountPassword(resetPasswordViewModel model)
        {
            if(ModelState.IsValid)
            {
               ApplicationUser user = await userManager.FindByNameAsync(User.Identity?.Name);

                if(user != null) 
                {
                    string token = await userManager.GeneratePasswordResetTokenAsync(user);
                    await userManager.ResetPasswordAsync(user, token , model.newPassword);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid user");
                return RedirectToAction("myAccount","_accountChangePasswordPartial" , model); // partial view only will return >> impossible for this case to match
            }
            return RedirectToAction("myAccount", new { selectedPartial = "_accountChangePasswordPartial", changePasswordModel = model}); // partial view only will return >> impossible for this case to match

           // return RedirectToAction("getAccountChangePasswordPartial", "account", model); // partial view only will return >> impossible for this case to match
        }

        //public IActionResult test()
        //{
        //    return View();
        //} 
    }
}
