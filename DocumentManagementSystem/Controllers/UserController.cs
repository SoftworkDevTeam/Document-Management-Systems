using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DocumentManagementSystem.Data;
using DocumentManagementSystem.Helper;
using DocumentManagementSystem.Repository;
using DocumentManagementSystem.ResponseModels;
using DocumentManagementSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DocumentManagementSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IAccessTypeRepository typeRepository;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IUserRepository profileRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly Utility utility;
        string userId = string.Empty;

        public UserController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IAccessTypeRepository typeRepository,
            RoleManager<IdentityRole> roleManager, IUserRepository profileRepository,
            IHttpContextAccessor httpContextAccessor, Utility utility)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.typeRepository = typeRepository;
            this.roleManager = roleManager;
            this.profileRepository = profileRepository;
            this.httpContextAccessor = httpContextAccessor;
            this.utility = utility;
            userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateUser()
        {
            ProfileViewModel model = new ProfileViewModel
            {
                CreatedBy = userId
            };
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateUser(ProfileViewModel viewModel)
        {
            //RegisterViewModel viewModel = new RegisterViewModel
            //{
            //    Title = "Registration"
            //};
            if (ModelState.IsValid)
            {
                string password = Utility.GenerateRandomPassword();
                var user = new ApplicationUser
                {
                    UserName = viewModel.Email,
                    Email = viewModel.Email,
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName
                };
                var result = await userManager.CreateAsync(user, password);
                var role = await roleManager.FindByNameAsync("user");
                result = await userManager.AddToRoleAsync(user, role.Name);
                if (result.Succeeded)
                {
                    viewModel.UserId = user.Id;
                    viewModel.Password = password;
                    ResponseModel response = await profileRepository.SaveAsync(viewModel);
                    
                    ViewBag.Title = "Registration Successful";
                    ViewBag.Message = response.Message;
                    ModelState.Clear();
                    utility.LogAudit((int)Utility.AuditAction.User_Creation, "Create User: FirstName: " + viewModel.FirstName + " LastName: "+viewModel.LastName, Request.Host.Value, userId);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    ViewBag.Message = error.Description;
                }
            }
            return View();
        }
        public async Task<IActionResult> AllUsers()
        {
            return View(await profileRepository.GetAllUserProfile());
        }
        [HttpGet]
        public async Task<IActionResult> UserAccess(long id)
        {
            UserAccessRequestViewModel userAccessRequest = await profileRepository.getProfileById(id);
            UserAccessRequestViewModel userAccess = new UserAccessRequestViewModel
            {
                UserProfileId=userAccessRequest.UserProfileId,
                FirstName = userAccessRequest.FirstName,
                LastName = userAccessRequest.LastName,
                AccessList = await typeRepository.GetAllAccessType(),
            };
            return View(userAccess);
        }
    }
}
