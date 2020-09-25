using DocumentManagementSystem.Data;
using System;
using System.Collections.Generic;

namespace DocumentManagementSystem.Models
{
    public partial class Document
    {
        public Document()
        {
            DocumentAccessRight = new HashSet<DocumentAccessRight>();
        }

        public long Id { get; set; }
        public string DocumentCode { get; set; }
        public string DocumentName { get; set; }
        public string Description { get; set; }
        public int? DocumentTypeId { get; set; }
        public long? OrganizationId { get; set; }
        public DateTime? RequestDate { get; set; }
        public string RequestBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string ApprovedBy { get; set; }
        public bool? IsCanceled { get; set; }
        public DateTime? CancelDate { get; set; }
        public string CanceledBy { get; set; }
        public string CancelReason { get; set; }
        public bool? IsFinalApprovalObtained { get; set; }
        public DateTime? LastUpdated { get; set; }
        public int? ApprovalStatusId { get; set; }

        public virtual ApprovalStatus ApprovalStatus { get; set; }
        public virtual ApplicationUser ApprovedByNavigation { get; set; }
        public virtual ApplicationUser CanceledByNavigation { get; set; }
        public virtual DocumentType DocumentType { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual ApplicationUser RequestByNavigation { get; set; }
        public virtual ICollection<DocumentAccessRight> DocumentAccessRight { get; set; }
    }
}
