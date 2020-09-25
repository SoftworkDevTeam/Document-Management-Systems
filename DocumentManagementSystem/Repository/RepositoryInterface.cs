using DocumentManagementSystem.Models;
using DocumentManagementSystem.ResponseModels;
using DocumentManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagementSystem.Repository
{
    public interface IDocumentTypeRepository
    {
        Task<ResponseModel> SaveDocumentTypeAsync(DocumentType model);
        Task<IEnumerable<DocumentType>> GetAllDocumentType();
        Task<DocumentType> FindByNameAsync(string name);
    }

    public interface IAccessTypeRepository
    {
        Task<ResponseModel> SaveAccessTypeAsync(AccessType model);
        Task<IEnumerable<AccessType>> GetAllAccessType();
        Task<AccessType> FindByNameAsync(string name);
    }
    public interface IApprovalStatusRepository
    {
        Task<ResponseModel> SaveStatusAsync(ApprovalStatus model);
        Task<IEnumerable<ApprovalStatus>> GetAllStaus();
        Task<ApprovalStatus> FindByNameAsync(string name);
    }
    public interface IAuditActionRepository
    {
        Task<ResponseModel> SaveActionAsync(AuditAction model);
        Task<IEnumerable<AuditAction>> GetAllAction();
        Task<AuditAction> FindByNameAsync(string name);
    }

    public interface IAuditTrailRepository
    {
        Task<IEnumerable<AuditRailViewModel>> GetAllAuditTrail();
    }
    public interface IApprovalProgressStatusRepository
    {
        Task<ResponseModel> SaveStatusAsync(ApprovalProgressStatus model);
        Task<IEnumerable<ApprovalProgressStatus>> GetAllStaus();
        Task<ApprovalProgressStatus> FindByNameAsync(string name);
    }

    public interface IUserRepository
    {
        Task<ResponseModel> SaveAsync(ProfileViewModel model);
        Task<ResponseModel> ConfirmEmail(string email);
        Task<ProfileViewModel> getProfileByUserId(string userId);
        Task<IEnumerable<ProfileViewModel>> GetAllUserProfile();
        Task<UserAccessRequestViewModel> getProfileById(long Id);
    }

    public interface INewDocumentRepository
    {
        Task<IEnumerable<DocumentViewModel>> GetAllPendingDocument();
        Task<DocumentViewModel> GetPendingDocumentById(long id);
        Task<ResponseModel> SaveAsync(DocumentViewModel model);
        Task<ResponseModel> UpdateAsync(DocumentViewModel model);
        Task<ResponseModel> DeleteAsync(long id);
    }
}
