using DocumentManagementSystem.Data;
using DocumentManagementSystem.Models;
using DocumentManagementSystem.ResponseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagementSystem.Repository
{
    public class ApprovalProgressStatusRepository : IApprovalProgressStatusRepository
    {
        private readonly AppDbContext dbContext;

        public ApprovalProgressStatusRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ApprovalProgressStatus> FindByNameAsync(string name)
        {
            return await dbContext.ApprovalProgressStatus.Where(x => x.Status.ToLower() == name.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ApprovalProgressStatus>> GetAllStaus()
        {
            return await dbContext.ApprovalProgressStatus.ToListAsync();
        }

        public async Task<ResponseModel> SaveStatusAsync(ApprovalProgressStatus model)
        {
            ResponseModel response = new ResponseModel();
            var newStatus = new ApprovalProgressStatus()
            {
                Status = model.Status
            };
            if (model.Status.Any())
            {
                dbContext.ApprovalProgressStatus.Add(newStatus);
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
                    dbContext.ApprovalProgressStatus.Local.Clear();
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
