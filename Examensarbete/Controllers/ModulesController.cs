﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ThesisProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using ThesisProject.Data;
using ThesisProject.Models;
using ThesisProject.Repositories;

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

        public FileStreamResult GetPDF()
        {
            FileStream fs = new FileStream("C:\\Users\\Olivia\\Desktop\\Olivia_Denbu_LIA-rapport_PROG17.pdf", FileMode.Open, FileAccess.Read);

            return File(fs, "application/pdf");
        }

        [HttpPost]
        public IActionResult Details(int id, string type)
        {
            FileStream fs = new FileStream("C:\\Users\\Olivia\\Desktop\\Olivia_Denbu_LIA-rapport_PROG17.pdf", FileMode.Open, FileAccess.Read);

            //TODO bryta ut Get include
            var module = _moduleRepository.Get(id);
            var viewModel = new ModuleViewModel
            {
                Name = module.Name,
                Facts = module.Facts.ToList(),
                Exams = module.ExamFile.ToList(),
                Exercises = module.ExerciseFile.ToList(),
                CurrentPDF = _fileRepository.GetCurrentFile(1, "GetFactsFileById")
        };

            switch (type)
            {
                case "facts":
                    //return File(fs, "application/pdf");
                    return PartialView("_FactsDetails", viewModel);
                case "exercises":
                    return PartialView("_ExerciseDetails", viewModel);
                case "exams":
                    return PartialView("_ExamDetails", viewModel);
                default:
                    break;
            }

            //TODO: Är detta rätt för fel(?)
            return View(viewModel);
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
                var file = new byte[0];

                switch (pdfType)
                {
                    case "facts":
                        file = _context.Facts
                            .Where(f => f.Id == fileId)
                            .Select(f => f.Content)
                            .SingleOrDefault();
                        break;
                    case "exercises":
                        file = _context.ExerciseFile
                            .Where(f => f.Id == fileId)
                            .Select(f => f.Content)
                            .SingleOrDefault();
                        break;
                    case "exams":
                        file = _context.ExamFile
                            .Where(f => f.Id == fileId)
                            .Select(f => f.Content)
                            .SingleOrDefault();
                        break;
                    default:
                        break;
                }

                //TODO: Byt ut C: till path
                using (var stream = new StreamWriter("C:\\Users\\Olivia\\Desktop\\" + fileName + "Test" + ".pdf"))
                {
                    var bw = new BinaryWriter(stream.BaseStream);
                    bw.Write(file);
                }

                return RedirectToAction("Course", "Home");
            }
        }
    }
}