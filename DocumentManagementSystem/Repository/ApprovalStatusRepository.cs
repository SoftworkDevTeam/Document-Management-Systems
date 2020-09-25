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
    public class ApprovalStatusRepository : IApprovalStatusRepository
    {
        private readonly AppDbContext dbContext;

        public ApprovalStatusRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ApprovalStatus> FindByNameAsync(string name)
        {
            return await dbContext.ApprovalStatus.Where(x => x.Status.ToLower() == name.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ApprovalStatus>> GetAllStaus()
        {
            return await dbContext.ApprovalStatus.ToListAsync();
        }

        public async Task<ResponseModel> SaveStatusAsync(ApprovalStatus model)
        {
            ResponseModel response = new ResponseModel();
            var newStatus = new ApprovalStatus()
            {
                Status = model.Status
            };
            if (model.Status.Any())
            {
                dbContext.ApprovalStatus.Add(newStatus);
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
                    dbContext.ApprovalStatus.Local.Clear();
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
