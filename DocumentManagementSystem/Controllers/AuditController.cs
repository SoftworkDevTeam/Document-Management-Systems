using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagementSystem.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DocumentManagementSystem.Controllers
{
    public class AuditController : Controller
    {
        private readonly IAuditTrailRepository trailRepository;

        public AuditController(IAuditTrailRepository trailRepository)
        {
            this.trailRepository = trailRepository;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            return View(await trailRepository.GetAllAuditTrail());
        }
    }
}
