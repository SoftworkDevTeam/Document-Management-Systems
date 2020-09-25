using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagementSystem.Data;
using DocumentManagementSystem.Models;
using DocumentManagementSystem.ResponseModels;
using Microsoft.EntityFrameworkCore;

namespace DocumentManagementSystem.Repository
{
    public class AuditActionRepository : IAuditActionRepository
    {
        private readonly AppDbContext dbContext;

        public AuditActionRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<AuditAction> FindByNameAsync(string name)
        {
            return await dbContext.AuditAction.Where(x => x.ActionName.ToLower() == name.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AuditAction>> GetAllAction()
        {
            return await dbContext.AuditAction.ToListAsync();
        }

        public async Task<ResponseModel> SaveActionAsync(AuditAction model)
        {
            ResponseModel response = new ResponseModel();
            var newAuditAction = new AuditAction()
            {
                ActionName = model.ActionName
            };
            if (model.ActionName.Any())
            {
                dbContext.AuditAction.Add(newAuditAction);
                try
                {
                    dbContext.SaveChanges();
                    response.Message = "Saved Successfully";
                    response.Code = 200;
                }
                catch (Exception ex)
                {
                    //Console.WriteLine($"Save Partner Status Error: {ex}");
                    response.Message = ex.Message;
                    response.Code = 404;
                    dbContext.AuditAction.Local.Clear();
                    ErrorLog log = new ErrorLog();
                    log.ErrorDate = DateTime.Now;
                    log.ErrorMessage = ex.Message;
                    log.ErrorSource = ex.Source;
                    log.ErrorStackTrace = ex.StackTrace;
                    dbContext.ErrorLogs.Add(log);
                    dbContext.SaveChanges();
                }
            }
            return response;
        }
    }
}
