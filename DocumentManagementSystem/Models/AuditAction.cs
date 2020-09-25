using System;
using System.Collections.Generic;

namespace DocumentManagementSystem.Models
{
    public partial class AuditAction
    {
        public AuditAction()
        {
            AuditTrail = new HashSet<AuditTrail>();
        }

        public int Id { get; set; }
        public string ActionName { get; set; }

        public virtual ICollection<AuditTrail> AuditTrail { get; set; }
    }
}
