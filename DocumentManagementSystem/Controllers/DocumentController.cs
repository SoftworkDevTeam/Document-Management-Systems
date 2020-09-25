using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DocumentManagementSystem.Repository;
using DocumentManagementSystem.ResponseModels;
using DocumentManagementSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DocumentManagementSystem.Controllers
{
    public class DocumentController : Controller
    {
        private readonly INewDocumentRepository documentRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IDocumentTypeRepository typeRepository;
        string userId = string.Empty;

        public DocumentController(INewDocumentRepository documentRepository, IHttpContextAccessor httpContextAccessor,
            IDocumentTypeRepository typeRepository)
        {
            this.documentRepository = documentRepository;
            this.httpContextAccessor = httpContextAccessor;
            this.typeRepository = typeRepository;
            userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        // GET: /<controller>/
        [HttpGet]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> CreateDocument()
        {
            DocumentViewModel model = new DocumentViewModel
            {
                RequestBy = userId,
                DocumentTypes = await typeRepository.GetAllDocumentType()
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> CreateDocument(DocumentViewModel model)
        {
            if (ModelState.IsValid)
            {
                //return BadRequest(ModelState);

                if (model.Document != null)
                {
                    //save document
                    string filename = "";
                    IFormFile file;
                    file = model.Document;
                    filename = Guid.NewGuid() + file.FileName.Replace(" ", "_");
                    string extension = Path.GetExtension(filename);
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads\\documents", filename);
                    var stream = new FileStream(path, FileMode.Create);
                    await file.CopyToAsync(stream);
                    model.DocumentName = "uploads/documents/" + filename;
                }
                else
                {
                    ViewBag.Message = "Document is missing";
                }
                ResponseModel response = await documentRepository.SaveAsync(model);
                if (response.Code == 200)
                {
                    ViewBag.Message = response.Message;
                    ModelState.Clear();
                }
                else
                {
                    ViewBag.ErrorMessage = "Error occured, kindly contact the administrator";
                }
            }
            DocumentViewModel documentView = new DocumentViewModel
            {
                RequestBy = userId,
                DocumentTypes = await typeRepository.GetAllDocumentType()
            };
            
            return View(documentView);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AllPendingRequest()
        {
            IEnumerable<DocumentViewModel> request = await documentRepository.GetAllPendingDocument();
            //ViewBag.PageTitle = "Vehicle Details";
            return View(request);
        }
    }
}
