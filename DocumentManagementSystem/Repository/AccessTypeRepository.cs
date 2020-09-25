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
    public class AccessTypeRepository : IAccessTypeRepository
    {
        private readonly AppDbContext dbContext;

        public AccessTypeRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<AccessType> FindByNameAsync(string name)
        {
            return await dbContext.AccessType.Where(x => x.Type.ToLower() == name.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AccessType>> GetAllAccessType()
        {
            return await dbContext.AccessType.ToListAsync();
        }

        public async Task<ResponseModel> SaveAccessTypeAsync(AccessType model)
        {
            ResponseModel response = new ResponseModel();
            var newAccessType = new AccessType()
            {
                Type = model.Type
            };
            if (model.Type.Any())
            {
                dbContext.AccessType.Add(newAccessType);
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
                    dbContext.AccessType.Local.Clear();
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
