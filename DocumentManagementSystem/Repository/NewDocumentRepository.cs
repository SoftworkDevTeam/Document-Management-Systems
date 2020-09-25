using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagementSystem.Data;
using DocumentManagementSystem.Helper;
using DocumentManagementSystem.Models;
using DocumentManagementSystem.ResponseModels;
using DocumentManagementSystem.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DocumentManagementSystem.Repository
{
    public class NewDocumentRepository : INewDocumentRepository
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public NewDocumentRepository(AppDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public async Task<ResponseModel> DeleteAsync(long id)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                NewDocumentRequest document = await dbContext.NewDocumentRequest.FindAsync(id);
                if (document != null)
                {
                    dbContext.NewDocumentRequest.Remove(document);
                    await dbContext.SaveChangesAsync();
                    response.Message = "Record deleted successfully";
                    response.Code = 200;
                }
                else
                {
                    response.Message = "Record not found";
                    response.Code = 404;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Code = 404;
                dbContext.NewDocumentRequest.Local.Clear();
                ErrorLog log = new ErrorLog();
                log.ErrorDate = DateTime.Now;
                log.ErrorMessage = ex.Message;
                log.ErrorSource = ex.Source;
                log.ErrorStackTrace = ex.StackTrace;
                dbContext.ErrorLogs.Add(log);
                await dbContext.SaveChangesAsync();
            }
            return response;
        }

        public async Task<IEnumerable<DocumentViewModel>> GetAllPendingDocument()
        {
            return await dbContext.NewDocumentRequest.OrderByDescending(x => x.Id).Where(x => x.ApprovalStatusId == (int)Utility.ApprovalStatus.Awaiting_Admin_Review).Select(x => new DocumentViewModel
            {
                ApprovalStatus = x.ApprovalStatus.Status,
                ApprovalStatusId = x.ApprovalStatusId,
                DocumentName = x.DocumentName,
                IsFinalApprovalObtained = x.IsFinalApprovalObtained,
                LastUpdated = x.LastUpdated,
                RequestBy = x.RequestByNavigation.FirstName + " " + x.RequestByNavigation.LastName,
                RequestDate = x.RequestDate,
                Description = x.Description,
                DocumentType = x.DocumentType.Type,
                DocumentTypeId = x.DocumentTypeId,
                Id = x.Id
            }).ToListAsync();
        }

        public async Task<DocumentViewModel> GetPendingDocumentById(long id)
        {
            return await dbContext.NewDocumentRequest.Where(x => x.Id == id).Select(x => new DocumentViewModel
            {
                ApprovalStatus = x.ApprovalStatus.Status,
                ApprovalStatusId = x.ApprovalStatusId,
                DocumentName = x.DocumentName,
                IsFinalApprovalObtained = x.IsFinalApprovalObtained,
                LastUpdated = x.LastUpdated,
                RequestBy = x.RequestByNavigation.FirstName + " " + x.RequestByNavigation.LastName,
                RequestDate = x.RequestDate,
                Description = x.Description,
                DocumentType = x.DocumentType.Type,
                DocumentTypeId = x.DocumentTypeId,
                Id = x.Id
            }).FirstOrDefaultAsync();
        }

        public async Task<ResponseModel> SaveAsync(DocumentViewModel model)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                var user = await userManager.FindByIdAsync(model.RequestBy);
                if (user != null)
                {
                    NewDocumentRequest document = new NewDocumentRequest()
                    {
                        DocumentTypeId = model.DocumentTypeId,
                        Description = model.Description,
                        ApprovalStatusId = (int)Utility.ApprovalStatus.Awaiting_Admin_Review,
                        DocumentName = model.DocumentName,
                        LastUpdated = DateTime.Now,
                        RequestBy = model.RequestBy,
                        RequestDate = DateTime.Now,
                        IsFinalApprovalObtained = false
                    };
                    dbContext.NewDocumentRequest.Add(document);
                    await dbContext.SaveChangesAsync();
                    response.Code = 200;
                    response.Message = "Document saved successfully, awaiting admin review";
                }
                else
                {
                    response.Code = 404;
                    response.Message = "User doesn't exist";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Code = 404;
                dbContext.NewDocumentRequest.Local.Clear();
                ErrorLog log = new ErrorLog();
                log.ErrorDate = DateTime.Now;
                log.ErrorMessage = ex.Message;
                log.ErrorSource = ex.Source;
                log.ErrorStackTrace = ex.StackTrace;
                dbContext.ErrorLogs.Add(log);
                await dbContext.SaveChangesAsync();
            }
            return response;
        }

        public async Task<ResponseModel> UpdateAsync(DocumentViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
