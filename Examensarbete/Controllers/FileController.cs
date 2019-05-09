using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ThesisProject.Data;
using ThesisProject.Models;
using ThesisProject.Repositories;

namespace ThesisProject.Controllers
{
    public class FileController : Controller
    {
        private ThesisProjectDBContext _context;
        private ModuleRepository _moduleRepository;

        public FileController(ThesisProjectDBContext context)
        {
            _context = context;
            //TODO dependendy injecton
            _moduleRepository = new ModuleRepository(_context);
        }

        public IActionResult Index()
        {
            return View();
        }

        //TODO: Ta bort om ovan funkar (annars den här pdf som är bra)
        public ActionResult Pdf()
        {
            //FileSeeder seeder = new FileSeeder(_context);
            //var bytes = seeder.Download();

            var path = "C:\\Users\\Olivia\\Desktop\\Olivia_Denbu_LIA-rapport_PROG17.pdf";
            path = "C:\\Users\\Olivia\\Desktop\\Test Veckobrev 1.pdf";

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
    }
}