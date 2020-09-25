using System;
using System.Collections.Generic;

namespace DocumentManagementSystem.Models
{
    public partial class ApprovalStatus
    {
        public ApprovalStatus()
        {
            Document = new HashSet<Document>();
            NewDocumentRequest = new HashSet<NewDocumentRequest>();
        }

        public int Id { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Document> Document { get; set; }
        public virtual ICollection<NewDocumentRequest> NewDocumentRequest { get; set; }
    }
}
