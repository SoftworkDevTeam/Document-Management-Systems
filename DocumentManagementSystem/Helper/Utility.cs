using DocumentManagementSystem.Data;
using DocumentManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagementSystem.Helper
{
    public class Utility
    {
        private readonly AppDbContext dbContext;
        private readonly IHttpContextAccessor httpContext;

        //private readonly HttpContext httpContext;

        public Utility(AppDbContext dbContext, IHttpContextAccessor httpContext)
        {
            this.dbContext = dbContext;
            this.httpContext = httpContext;
        }
        public static string GenerateRandomPassword(PasswordOptions opts = null)
        {
            if (opts == null) opts = new PasswordOptions()
            {
                RequiredLength = 8,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = true,
                RequireUppercase = true
            };

            string[] randomChars = new[] {
        "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
        "abcdefghijkmnopqrstuvwxyz",    // lowercase
        "0123456789",                   // digits
        "!@$?_-"                        // non-alphanumeric
    };
            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }

        public void LogAudit(int ActionId,string Remark, string AffectedWebPage, string UserId)
        {
            string ipAddress = string.Empty;

            if (httpContext.HttpContext.Connection.RemoteIpAddress != null)
            {
                ipAddress = httpContext.HttpContext.Connection.RemoteIpAddress.ToString();
            }
            else
            {
                ipAddress = httpContext.HttpContext.Connection.LocalIpAddress.ToString();
            }
            AuditTrail auditTrail = new AuditTrail
            {
                ActionDate = DateTime.Now,
                ActionId = ActionId,
                AffectedWebPage = AffectedWebPage,
                Ipaddress = ipAddress,
                Remark = Remark,
                UserId = UserId
            };
            dbContext.AuditTrail.Add(auditTrail);
            dbContext.SaveChanges();
        }
        public enum AccessType
        {
            Create = 1,
            Read = 2,
            Update = 3,
            Delete = 4,
            Download = 5,
        };
        public enum ApprovalStatus
        {
            Awaiting_Admin_Review = 1,
            Approved = 2,
            Declined = 3,
            Queried_By_Admin = 4,
        };
        public enum ApprovalProgressStatus
        {
            Approval_In_Progress = 1,
            Approved = 2,
            Declined = 3,
            Queried = 4,
        };
        public enum AuditAction
        {
            Add = 1,
            Delete = 2,
            Disable = 3,
            Edit = 4,
            Enable = 5,
            Login = 6,
            Password_Reset = 7,
            Upload = 8,
            Download = 9,
            User_Creation = 10,
        };
    }
}
