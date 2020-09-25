using System;
using System.Collections.Generic;

namespace DocumentManagementSystem.Models
{
    public partial class DocumentType
    {
        public DocumentType()
        {
            Document = new HashSet<Document>();
            NewDocumentRequest = new HashSet<NewDocumentRequest>();
        }

        public int Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Document> Document { get; set; }
        public virtual ICollection<NewDocumentRequest> NewDocumentRequest { get; set; }
    }
}
