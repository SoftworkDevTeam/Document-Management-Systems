using DocumentManagementSystem.Data;
using System;
using System.Collections.Generic;

namespace DocumentManagementSystem.Models
{
    public partial class DocumentAccessRight
    {
        public long Id { get; set; }
        public long? DocumentId { get; set; }
        public string RoleId { get; set; }
        public string UserId { get; set; }
        public int? AccessTypeId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? LastUpdated { get; set; }

        public virtual AccessType AccessType { get; set; }
        public virtual ApplicationUser CreatedByNavigation { get; set; }
        public virtual Document Document { get; set; }
        //public virtual AspNetRoles Role { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
