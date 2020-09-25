using DocumentManagementSystem.Data;
using System;
using System.Collections.Generic;

namespace DocumentManagementSystem.Models
{
    public partial class AuditTrail
    {
        public long Id { get; set; }
        public int ActionId { get; set; }
        public string Ipaddress { get; set; }
        public DateTime ActionDate { get; set; }
        public string Remark { get; set; }
        public string AffectedWebPage { get; set; }
        public string UserId { get; set; }

        public virtual AuditAction Action { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
