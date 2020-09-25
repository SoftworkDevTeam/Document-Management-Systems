using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagementSystem.Repository;
using DocumentManagementSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagementSystem.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly INewDocumentRepository documentRepository;

        public DashboardController(IUserRepository userRepository, INewDocumentRepository documentRepository)
        {
            this.userRepository = userRepository;
            this.documentRepository = documentRepository;
        }
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> Index()
        {
            PageViewModel PageData = new PageViewModel()
            {
                Title = "Dashboard",
                Description = "Admin Area dashboard",
                Active = "active",
                Area = "dashboard",
                ProfileList = await userRepository.GetAllUserProfile(),
                NewDocList = await documentRepository.GetAllPendingDocument()
            };
            return View(PageData);
        }
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> AuditTrail()
        {
            PageViewModel PageData = new PageViewModel()
            {
                Title = "Dashboard",
                Description = "Admin Area dashboard",
                Active = "active",
                Area = "dashboard",
                ProfileList = await userRepository.GetAllUserProfile(),
                NewDocList = await documentRepository.GetAllPendingDocument()
            };
            return View(PageData);
        }
    }
}