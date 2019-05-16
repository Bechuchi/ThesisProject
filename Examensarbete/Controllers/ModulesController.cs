using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
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

        public ModulesController(ThesisProjectDBContext context)
        {
            _context = context;
            //TODO interface 
            _moduleRepository = new ModuleRepository(_context);
            _fileRepository = new FileRepository(_context);
        }


        [HttpPost]
        public IActionResult Details(int id, string type)
        {
            var viewModel = new DetailsViewModel
            {
                PdfType = type,
                FileId = id
            };

            //TODO bryta ut Get include
            //var module = _moduleRepository.Get(id);

            return PartialView("_Details", viewModel);
        }


        //[HttpPost]
        //public IActionResult Details(int id, string type)
        //{
        //    //TODO bryta ut Get include
        //    var module = _moduleRepository.Get(id);
        //    var viewModel = new ModuleViewModel
        //    {
        //        Name = module.Name,
        //        Facts = module.Facts.ToList(),
        //        Exams = module.ExamFile.ToList(),
        //        Exercises = module.ExerciseFile.ToList(),
        //        CurrentPDF = _fileRepository.GetCurrentFile(1, "GetFactsFileById")
        //    };

        //    switch (type)
        //    {
        //        case "facts":
        //            //return File(fs, "application/pdf");
        //            return PartialView("_FactsDetails", viewModel);
        //        case "exercises":
        //            return PartialView("_ExerciseDetails", viewModel);
        //        case "exams":
        //            return PartialView("_ExamDetails", viewModel);
        //        default:
        //            break;
        //    }

        //    //TODO: Fel måste fixas
        //    return View(viewModel);
        //}

        public ActionResult DisplayImage()
        {
            var image = _fileRepository.GetCurrentFile(2, "GetImageById");

            return new FileContentResult(image, "application/jpg");
        }

        public ActionResult BrowsePdf(int fileId, string pdfType)
        {
            string cmdText = "";

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

            return new FileContentResult(file, "application/pdf");
        }

        public ActionResult Download(int fileId, string pdfType, string fileName)
        {
            //TODO: Fixa connsträng
            //TODO: Fixa path
            //TODO: Fixa fråga vid download

            var connectionString = "Server=localhost;Database=ThesisProjectDB;Integrated Security=True;";

            using (var connection = new SqlConnection(connectionString))
            {                
                var file = _fileRepository.GetFileToDownload(fileId, pdfType);
                
                //TODO: Fixa path
                //var path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + fileName + ".pdf";
                var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);


                //TODO: Byt ut C: till path
                using (var stream = new StreamWriter(path/*"C:\\Users\\Olivia\\Desktop\\" + fileName + "Test" + ".pdf"*/))
                {
                    var bw = new BinaryWriter(stream.BaseStream);
                    bw.Write(file);
                }

                return RedirectToAction("Course", "Home");
            }
        }
    }
}