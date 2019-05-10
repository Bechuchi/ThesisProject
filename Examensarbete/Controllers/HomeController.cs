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

        //Localization #2
        ////TODO: Ta bort om inte funkar
        //private string CurrentLanguage
        //{
        //    get
        //    {
        //        if (!string.IsNullOrEmpty(_currentLanguage))
        //        {
        //            return _currentLanguage;
        //        }

        //        if (RouteData.Values.ContainsKey("lang"))
        //        {
        //            _currentLanguage = RouteData.Values["lang"].ToString().ToLower();

        //            if (_currentLanguage == "ee")
        //            {
        //                _currentLanguage = "et";
        //            }
        //        }

        //        if (string.IsNullOrEmpty(_currentLanguage))
        //        {
        //            var feature = HttpContext.Features.Get<IRequestCultureFeature>();

        //            _currentLanguage = feature.RequestCulture.Culture.TwoLetterISOLanguageName.ToLower();
        //        }

        //        return _currentLanguage;
        //        }
        //    }


        //Localization #2
        ////TODO: Ta bort om inte funkar
        //public ActionResult RedirectToDefaultLanguage()
        //{
        //    var lang = CurrentLanguage;

        //    if (lang == "et")
        //    {
        //        lang = "ee";
        //    }

        //    return RedirectToAction("Index", new { lang = lang });
        //}

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

        public IActionResult Index()
        {
            //Så jag har seedad med pdf
            //FileSeeder sd = new FileSeeder(_context);
            //sd.SeedDbWithExamFile();

            //Download
            var seeder = new FileSeeder(_context);
            //seeder.GetFile();
            seeder.Download();

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

        //Denna kan göra så formatet av en PDF visas i browsern
        //TODO: Få tag i filen från db
        public ActionResult Pdf()
        {
            var path = "C:\\Users\\Olivia\\Desktop\\Olivia_Denbu_LIA-rapport_PROG17.pdf";

            var fileStream = new FileStream(path,
                                            FileMode.Open,
                                            FileAccess.Read
                                            );

            var fsResult = new FileStreamResult(fileStream, "application/pdf");
            //var fsResult = new FileStreamResult(fileStream, "application/pdf");
            FileSeeder sd = new FileSeeder(_context);
            var bytes = sd.GetFile();

            return new FileContentResult(bytes, "application/pdf");
            //return fsResult;
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
