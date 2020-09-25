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
using Microsoft.Extensions.Configuration;

namespace DocumentManagementSystem.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext dbContext;
        private readonly IConfiguration configuration;
        private readonly Mailer mailer;
        private readonly UserManager<ApplicationUser> userManager;

        public UserRepository(AppDbContext dbContext, IConfiguration configuration, Mailer mailer, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.configuration = configuration;
            this.mailer = mailer;
            this.userManager = userManager;
        }

        public async Task<ResponseModel> ConfirmEmail(string email)
        {
            ResponseModel responseModels = new ResponseModel();
            try
            {
                var user = await userManager.FindByEmailAsync(email);
                if (user != null)
                {
                    user.EmailConfirmed = true;
                    await userManager.UpdateAsync(user);
                    responseModels.Message = "Email Confirmed";
                    responseModels.Code = 200;
                }
                else
                {
                    responseModels.Message = "Email Confirmation Failed";
                    responseModels.Code = 404;
                }
            }
            catch (Exception ex)
            {
                dbContext.UserProfiles.Local.Clear();
                //Console.WriteLine($"Save Profile Error: {ex}");
                ErrorLog log = new ErrorLog();
                log.ErrorDate = DateTime.Now;
                log.ErrorMessage = ex.Message;
                log.ErrorSource = ex.Source;
                log.ErrorStackTrace = ex.StackTrace;
                dbContext.ErrorLogs.Add(log);
                dbContext.SaveChanges();
                responseModels.Code = 404;
                responseModels.Message = ex.Message;
            }
            return responseModels;
        }

        public async Task<IEnumerable<ProfileViewModel>> GetAllUserProfile()
        {
            return await dbContext.UserProfiles.Select(x => new ProfileViewModel
            {
                Id = x.Id,
                CreatedBy = x.CreatedBy,
                Email = x.Email,
                DateCreated = x.DateCreated,
                DateUpdated = x.DateUpdated,
                FirstName = x.FirstName,
                IsActive = x.IsActive,
                LastName = x.LastName,
                MobileNumber = x.MobileNumber,
                UserId = x.UserId
            }).ToListAsync();
        }

        public async Task<UserAccessRequestViewModel> getProfileById(long Id)
        {
            return await dbContext.UserProfiles.Where(x=>x.Id == Id).Select(x => new UserAccessRequestViewModel
            {
                UserProfileId = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName
            }).FirstOrDefaultAsync();
        }

        public Task<ProfileViewModel> getProfileByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel> SaveAsync(ProfileViewModel model)
        {
            ResponseModel response = new ResponseModel();
            //save user profile
            var newProfile = new UserProfile()
            {
                UserId = model.UserId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                MobileNumber = model.MobileNumber,
                IsActive = true,
                CreatedBy = model.CreatedBy,
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now
            };
            if (model.UserId.Any())
            {
                dbContext.UserProfiles.Add(newProfile);
                try
                {
                    await dbContext.SaveChangesAsync();
                    response.Code = 200;
                    response.Message = "User Created Successfully";
                    string confirmationLink = configuration["Mailing:Url"] + "Account/confirmEmail?email=" + model.Email;
                    mailer.UserRegistration(configuration, model.FirstName, model.Email, model.Password, confirmationLink);
                }
                catch (Exception ex)
                {
                    dbContext.UserProfiles.Local.Clear();
                    //Console.WriteLine($"Save Profile Error: {ex}");
                    ErrorLog log = new ErrorLog();
                    log.ErrorDate = DateTime.Now;
                    log.ErrorMessage = ex.Message;
                    log.ErrorSource = ex.Source;
                    log.ErrorStackTrace = ex.StackTrace;
                    dbContext.ErrorLogs.Add(log);
                    dbContext.SaveChanges();
                    response.Code = 404;
                    response.Message = ex.Message;
                }
            }
            return response;
        }
    }
}
