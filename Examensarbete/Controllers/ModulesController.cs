﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using ThesisProject.Models;
using ThesisProject.Repositories;
using ThesisProject.ViewModels;

namespace ThesisProject.Controllers
{
    public class ModulesController : Controller
    {
        private ThesisProjectDBContext _context;
        private ModuleRepository _moduleRepository;
        private readonly FileRepository _fileRepository;
        private readonly IConfiguration _configuration;

        public ModulesController(ThesisProjectDBContext context,
                                 IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            //TODO interface 
            _moduleRepository = new ModuleRepository(_context);
            _fileRepository = new FileRepository(_context, _configuration);
        }


        [HttpPost]
        public IActionResult Details(int id, string type)
        {
            var viewModel = new DetailsViewModel
            {
                PdfType = type,
                FileId = id
            };

            return PartialView("_Details", viewModel);
        }

        public ActionResult DisplayImage(int id)
        {
            var image = _fileRepository.GetCurrentFile(id, "GetImageById");

            return new FileContentResult(image, "application/jpg");
        }

        public ActionResult BrowsePdf(int fileId, string pdfType)
        {
            string cmdText = "";

            //var currentLanguage = _fileRepository.GetCurrentLanguage();

            //var fileLang = _context.ExamFile
            //                .Where(e => e.Id == fileId && e.Language == "fr")
            //                .Select(e => e.Content)
            //                .SingleOrDefault();

            switch (pdfType)
            {
                case "facts":
                    cmdText = "GetFactsFileById";
                    break;
                case "exercises":
                    cmdText = "GetExerciseFileById";
                    break;
                case "exams":
                    cmdText = "GetExamFileById";
                    break;
                default:
                    break;
            }

            var file = _fileRepository.GetCurrentFile(fileId, cmdText);

            //return new FileContentResult(fileLang, "application/pdf");
            return new FileContentResult(file, "application/pdf");
        }

        public ActionResult Download(int fileId, string pdfType, string fileName)
        {
            //TODO: Fixa fråga vid download
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (var connection = new SqlConnection(connectionString))
            {                
                var file = _fileRepository.GetFileToDownload(fileId, pdfType);
                var cd = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileNameStar = fileName + ".pdf"
                    //FileNameStar = "download.pdf"
                };

                Response.Headers.Add(HeaderNames.ContentDisposition, cd.ToString());

                return File(file, "application/pdf");
            }
        }
    }
}