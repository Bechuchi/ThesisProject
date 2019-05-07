using System;
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

        public ModulesController(ThesisProjectDBContext context)
        {
            _context = context;
            //TODO dependendy injecton
            _moduleRepository = new ModuleRepository(_context);
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

        public string ReadTextfile()
        {
            var path = "C:\\Users\\Olivia\\Desktop\\Olivia_Denbu_LIA-rapport_PROG17.pdf";

            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(path))
                {
                    // Read the stream to a string, and write the string to the console.
                    //TODO: Annat sätt för att visa bild/pdf: är en blob elr ngt (inte sträng)
                    String line = sr.ReadToEnd();

                    return line;
                }
            }
            catch (IOException e)
            {
                return e.ToString();
            }
        }

        [HttpPost]
        public IActionResult Details(int id)
        {
            var module = _moduleRepository.GetCurrentModule(id);
            var viewModel = new ModuleViewModel
            {
                Name = module.Name,
                Facts = module.Facts,
                Exams = module.ExamFile.ToList()
            };         

            return PartialView("_Details", viewModel);
        }
    }
}