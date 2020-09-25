using DocumentManagementSystem.Data;
using System;
using System.Collections.Generic;

namespace DocumentManagementSystem.Models
{
    public partial class GrantedApproval
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string Comment { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public int? ApprovalProgressId { get; set; }
        public long? DocumentRequestId { get; set; }

        public virtual ApprovalProgressStatus ApprovalProgress { get; set; }
        public virtual NewDocumentRequest DocumentRequest { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
