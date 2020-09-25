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
    public class DocumentTypeRepository : IDocumentTypeRepository
    {
        private readonly AppDbContext dbContext;

        public DocumentTypeRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<DocumentType> FindByNameAsync(string name)
        {
            return await dbContext.DocumentType.Where(x => x.Type.ToLower() == name.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<DocumentType>> GetAllDocumentType()
        {
            return await dbContext.DocumentType.ToListAsync();
        }

        public async Task<ResponseModel> SaveDocumentTypeAsync(DocumentType model)
        {
            ResponseModel response = new ResponseModel();
            var newDocType = new DocumentType()
            {
                Type = model.Type
            };
            if (model.Type.Any())
            {
                dbContext.DocumentType.Add(newDocType);
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
                    dbContext.DocumentType.Local.Clear();
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
