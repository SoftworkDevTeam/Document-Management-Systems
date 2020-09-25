using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagementSystem.Data;
using DocumentManagementSystem.Models;
using DocumentManagementSystem.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DocumentManagementSystem.Repository
{
    public class AuditTrailRepository : IAuditTrailRepository
    {
        private readonly AppDbContext dbContext;

        public AuditTrailRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<AuditRailViewModel>> GetAllAuditTrail()
        {
            return await dbContext.AuditTrail.OrderByDescending(x => x.Id).Select(x => new AuditRailViewModel
            {
                ActionId = x.ActionId,
                ActionName = x.Action.ActionName,
                DateCreated = x.ActionDate,
                IpAddress = x.Ipaddress,
                Remark = x.Remark,
                UserId = x.UserId,
                UserName = x.User.FirstName + " " + x.User.LastName,
                Id = x.Id,
                WebPage = x.AffectedWebPage
            }).ToListAsync();
        }
    }

}
