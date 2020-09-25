using DocumentManagementSystem.Data;
using DocumentManagementSystem.Models;
using DocumentManagementSystem.Repository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagementSystem.Data
{
    public class SeedData
    {
        private readonly AppDbContext dbContext;

        public SeedData(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public static void SeedDatas(
            RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager,
            IDocumentTypeRepository typeRepository, IAccessTypeRepository accessType,
            IApprovalProgressStatusRepository approvalProgress, IApprovalStatusRepository approvalStatus,
            IAuditActionRepository actionRepository
            )
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
            SeedDocumentType(typeRepository);
            SeedAccessType(accessType);
            SeedApprovalProgressStatus(approvalProgress);
            SeedApprovalStatus(approvalStatus);
            SeedAuditAction(actionRepository);
        }
        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("admin").Result)
            {
                IdentityRole adminRole = new IdentityRole();

                adminRole.Name = "admin";
                adminRole.NormalizedName = "ADMIN";
                IdentityResult adminRoleResult = roleManager.CreateAsync(adminRole).Result;
            }
            if (!roleManager.RoleExistsAsync("user").Result)
            {
                IdentityRole apiUserRole = new IdentityRole();

                apiUserRole.Name = "user";
                apiUserRole.NormalizedName = "USER";
                IdentityResult apiUserRoleResult = roleManager.CreateAsync(apiUserRole).Result;
            }
        }

        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {

            if (userManager.FindByNameAsync("admin").Result == null)
            {

                ApplicationUser adminUser = new ApplicationUser();
                adminUser.UserName = "admin@admin.com";
                adminUser.Email = "admin@admin.com";
                adminUser.EmailConfirmed = true;

                try
                {
                    IdentityResult adminUserResult = userManager.CreateAsync(adminUser, "password").Result;
                    if (adminUserResult.Succeeded)
                    {

                        userManager.AddToRoleAsync(adminUser, "admin").Wait();
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

            }
        }
        public static void SeedDocumentType(IDocumentTypeRepository typeRepository)
        {
            string[] docTypeList = new string[] { "Invoice", "Legal", "Logistics", "Recruitment", "Other" };
            foreach (string docType in docTypeList)
            {
                if (typeRepository.FindByNameAsync(docType).Result == null)
                {
                    DocumentType documentType = new DocumentType()
                    {
                        Type = docType
                    };
                    try
                    {
                        typeRepository.SaveDocumentTypeAsync(documentType);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
        }
        public static void SeedAccessType(IAccessTypeRepository typeRepository)
        {
            string[] accessTypeList = new string[] { "Create", "Read", "Update", "Delete", "Download" };
            foreach (string accessType in accessTypeList)
            {
                if (typeRepository.FindByNameAsync(accessType).Result == null)
                {
                    AccessType access = new AccessType()
                    {
                        Type = accessType
                    };
                    try
                    {
                        typeRepository.SaveAccessTypeAsync(access);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
        }
        public static void SeedAuditAction(IAuditActionRepository actionRepository)
        {
            string[] auditActionList = new string[] { "Add", "Delete", "Disable", "Edit", "Enable",
                                                        "Login", "Password Reset", "Upload", "Download", "User Creation" };
            foreach (string action in auditActionList)
            {
                if (actionRepository.FindByNameAsync(action).Result == null)
                {
                    AuditAction auditAction = new AuditAction()
                    {
                        ActionName = action
                    };
                    try
                    {
                        actionRepository.SaveActionAsync(auditAction);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
        }
        public static void SeedApprovalStatus(IApprovalStatusRepository statusRepository)
        {
            string[] statusList = new string[] { "Awaiting Admin Review", "Approved", "Declined", "Queried By Admin" };
            foreach (string status in statusList)
            {
                if (statusRepository.FindByNameAsync(status).Result == null)
                {
                    ApprovalStatus approval = new ApprovalStatus()
                    {
                        Status = status
                    };
                    try
                    {
                        statusRepository.SaveStatusAsync(approval);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
        }
        public static void SeedApprovalProgressStatus(IApprovalProgressStatusRepository statusRepository)
        {
            string[] statusList = new string[] { "Approval In Progress", "Approved", "Declined", "Queried" };
            foreach (string status in statusList)
            {
                if (statusRepository.FindByNameAsync(status).Result == null)
                {
                    ApprovalProgressStatus approval = new ApprovalProgressStatus()
                    {
                        Status = status
                    };
                    try
                    {
                        statusRepository.SaveStatusAsync(approval);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
        }
    }
}
