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
using System.IO.Compression;
using iTextSharp.text;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Http;
using ThesisProject.StrategyPattern;
using System.Data.SqlClient;
using System.Data;

namespace ThesisProject.Controllers
{
    public class HomeController : Controller
    {
        private ThesisProjectDBContext _context;
        private ModuleRepository _moduleRepository;

        private readonly IStringLocalizer<HomeController> _localizer;
        //TODO: Kanske ta bort
        //private string _currentLanguage;

        public HomeController(ThesisProjectDBContext context,
                              IStringLocalizer<HomeController> localizer)
        {
            _context = context;
            //TODO dependendy injecton
            _moduleRepository = new ModuleRepository(_context);
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                Heading = "Welcome",
                AboutUs = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like)."
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

        public IActionResult Course()
        {
            //TODO: Move process of seeding
            var seeder = new FileSeeder(_context);
            //seeder.SeedDbWithImage();
            //seeder.SeedDbWithExerciseFile();
            //seeder.SeedDbWithExamFile();
            //seeder.SeedDbWithFactsFile();

            //TODO lägga i repo(?)
            var course = _context.Course
                .FirstOrDefault();

            var modules = _moduleRepository.GetModulesForCourse(course.Id); ;
            
            var viewModel = new CourseViewModel
            {
                Name = course.Name,
                Modules = modules
            };

            ViewData["MyTitle"] = _localizer["The localised title of my app!"];

            return View(viewModel);
        }

        //TODO: Ta bort när pdf är fixat
        //Läser upp ett pdf dokument i browsern men texten har skrivits in från action metoden
        //public ActionResult PdfTest()
        //{
        //    MemoryStream workStream = new MemoryStream();
        //    Document document = new Document();
        //    PdfWriter.GetInstance(document, workStream).CloseStream = false;

        //    document.Open();
        //    document.Add(new Paragraph("Hello World"));
        //    document.Add(new Paragraph(DateTime.Now.ToString()));
        //    document.Close();

        //    byte[] byteInfo = workStream.ToArray();
        //    workStream.Write(byteInfo, 0, byteInfo.Length);
        //    workStream.Position = 0;

        //    return new FileStreamResult(workStream, "application/pdf");
        //}

        //TODO: Ta bort när pdf funkar (förmodligen onödig då jag ska få upp min pdf från db och inte som bytes)
        //public bool ByteArrayToFile()
        //{
        //    var fileName = "OliviasHejjarklack";
        //    //FileSeeder seeder = new FileSeeder(_context);
        //    //var byteArray = seeder.Download();

        //    //try
        //    //{
        //    //    using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
        //    //    {
        //    //        fs.Write(byteArray, 0, byteArray.Length);
        //    //        return true;
        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    Console.WriteLine("Exception caught in process: {0}", ex);
        //    //    return false;
        //    //}
        //}
    }
}
