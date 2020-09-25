using DocumentManagementSystem.Models;
using GoTransport.Helper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagementSystem.ViewModels
{
    public class DocumentViewModel
    {
        public long Id { get; set; }
        public string DocumentName { get; set; }
        [Display(Name = "Documment")]
        [Required(ErrorMessage = "Please select a file")]
        [AllowedExtensions(new string[] { ".jpg", ".png", ".gif", ".jpeg", ".pdf", ".doc", ".docx", ".xls", ".xlsx" })]
        public IFormFile Document { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Documment Type")]
        public int? DocumentTypeId { get; set; }
        public string DocumentType { get; set; }
        public IEnumerable<DocumentType> DocumentTypes { get; set; }
        public long? OrganizationId { get; set; }
        public DateTime? RequestDate { get; set; }
        public string RequestBy { get; set; }
        public bool? IsCanceled { get; set; }
        public DateTime? CancelDate { get; set; }
        public string CanceledBy { get; set; }
        public string CancelReason { get; set; }
        public bool? IsFinalApprovalObtained { get; set; }
        public DateTime? LastUpdated { get; set; }
        public int? ApprovalStatusId { get; set; }
        public string ApprovalStatus { get; set; }
    }
}
