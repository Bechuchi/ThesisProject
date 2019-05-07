using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ThesisProject.ViewModels;
using ThesisProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using ThesisProject.Data;
using ThesisProject.Repositories;
using System.Net;
using System.IO;

namespace ThesisProject.Controllers
{
    public class HomeController : Controller
    {
        private ThesisProjectDBContext _context;
        private ModuleRepository _moduleRepository;

        public HomeController(ThesisProjectDBContext context)
        {
            _context = context;
            //TODO dependendy injecton
            _moduleRepository = new ModuleRepository(_context);
        }

        

        public IActionResult Index()
        {
            //TODO lägga i repo(?)
            var course = _context.Course
                .FirstOrDefault();

            var modules = _moduleRepository.GetModulesForCourse(course.Id);         ;

            var viewModel = new CourseViewModel
            {
                Name = course.Name,
                Modules = modules
            };

            
            return View(viewModel);
        }

        public ActionResult Pdf()
        {
            FileSeeder seeder = new FileSeeder(_context);
            var bytes = seeder.Download();

            var path = "C:\\Users\\Olivia\\Desktop\\Olivia_Denbu_LIA-rapport_PROG17.pdf";
            var fileStream = new FileStream(path,
                                     FileMode.Open,
                                     FileAccess.Read
                                   );

            var fsResult = new FileStreamResult(fileStream, "application/pdf");
            return fsResult;


            //FileSeeder seeder = new FileSeeder(_context);
            //var bytes = seeder.Download();

            ////byte[] bytes = GetYourByteArrayForPDF();
            //return File(bytes, "application/pdf", "somefriendlyname.pdf");
        }

        //public File Pdf()
        //{
        //    return File(stream, fileName, "application/pdf")
        //}

            //TODO fixa seeding rätt
            //FileSeeder seeder = new FileSeeder();
            //seeder.Download()
        }
}
