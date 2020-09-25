using System;
using System.Collections.Generic;

namespace DocumentManagementSystem.Models
{
    public partial class ApprovalProgressStatus
    {
        public ApprovalProgressStatus()
        {
            GrantedApproval = new HashSet<GrantedApproval>();
        }

        public int Id { get; set; }
        public string Status { get; set; }

        public virtual ICollection<GrantedApproval> GrantedApproval { get; set; }
    }
}
