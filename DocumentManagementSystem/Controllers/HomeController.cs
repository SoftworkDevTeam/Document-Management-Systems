using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DocumentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using DocumentManagementSystem.ViewModels;
using Microsoft.AspNetCore.Identity;
using DocumentManagementSystem.Data;
using DocumentManagementSystem.Helper;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace DocumentManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly Utility utility;
        private readonly IHttpContextAccessor httpContextAccessor;
        string userId = string.Empty;

        public HomeController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager, Utility utility,
            IHttpContextAccessor httpContextAccessor)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.utility = utility;
            this.httpContextAccessor = httpContextAccessor;
            userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            if (signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Index(LoginViewModel viewModel, string returnUrl)
        {
            try
            {
                viewModel.ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
                if (ModelState.IsValid)
                {
                    var user = await userManager.FindByEmailAsync(viewModel.Email);
                    if (user != null && !user.EmailConfirmed && (await userManager.CheckPasswordAsync(user, viewModel.Password)))
                    {
                        ModelState.AddModelError("", "Email not confirmed yet");
                        ViewBag.Message = "Email not confirmed yet";
                        return View(viewModel);
                    }
                    var result = await signInManager.PasswordSignInAsync(viewModel.Email, viewModel.Password, viewModel.RememberMe, true);
                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            utility.LogAudit((int)Utility.AuditAction.Login,"User Login: UserName: "+user.UserName, Request.Host.Value, user.Id);
                            return RedirectToAction("Index", "Dashboard");
                        }
                    }
                    ModelState.AddModelError("", "Invalid Login attempt");
                    ViewBag.Message = "Invalid Login attempt";
                }
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
