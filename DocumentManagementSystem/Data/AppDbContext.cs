using System;
using DocumentManagementSystem.Models;
using DocumentManagementSystem.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DocumentManagementSystem.Data
{
    public partial class AppDbContext : IdentityDbContext<ApplicationUser>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccessType> AccessType { get; set; }
        public virtual DbSet<ApprovalProgressStatus> ApprovalProgressStatus { get; set; }
        public virtual DbSet<ApprovalStatus> ApprovalStatus { get; set; }
        public virtual DbSet<AuditAction> AuditAction { get; set; }
        public virtual DbSet<AuditTrail> AuditTrail { get; set; }
        public virtual DbSet<Document> Document { get; set; }
        public virtual DbSet<DocumentAccessRight> DocumentAccessRight { get; set; }
        public virtual DbSet<DocumentSearchType> DocumentSearchType { get; set; }
        public virtual DbSet<DocumentType> DocumentType { get; set; }
        public virtual DbSet<GrantedApproval> GrantedApproval { get; set; }
        public virtual DbSet<NewDocumentRequest> NewDocumentRequest { get; set; }
        public virtual DbSet<Organization> Organization { get; set; }
        public virtual DbSet<ErrorLog> ErrorLogs { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }

    }
}
