using System;
using System.Collections.Generic;

namespace DocumentManagementSystem.Models
{
    public partial class Organization
    {
        public Organization()
        {
            Document = new HashSet<Document>();
            NewDocumentRequest = new HashSet<NewDocumentRequest>();
        }

        public long Id { get; set; }
        public string Organization1 { get; set; }

        public virtual ICollection<Document> Document { get; set; }
        public virtual ICollection<NewDocumentRequest> NewDocumentRequest { get; set; }
    }
}
