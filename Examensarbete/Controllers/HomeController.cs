﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ThesisProject.ViewModels;
using ThesisProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using ThesisProject.Data;
using ThesisProject.Repositories;
using System.Net;
using System.IO;
using System.IO.Compression;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

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
        
        public IActionResult Today()
        {
            var course = _context.Course
                .FirstOrDefault();

            var modules = _moduleRepository.GetModulesForCourse(course.Id); ;

            var viewModel = new CourseViewModel
            {
                Name = course.Name,
                ModulesVM = modules.Select(r => new ModuleViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    PartsOfModule = new List<PartOfModule>()
                    {
                        new PartOfModule
                        {
                            Id = 1,
                            PartType = "facts",
                            FactsNew = r.Facts.Select(f => new FactViewModel
                            {
                                Id = f.Id,
                                Name = f.Name
                            }).ToList()
                        },
                        new PartOfModule
                        {
                            Id = 2,
                            PartType = "exercises"
                        },
                        new PartOfModule
                        {
                            Id = 3,
                            PartType = "exams",
                            ExamsNew = r.ExamFile.Select(e => new ExamViewModel
                            {
                                Id = e.Id,
                                Name = e.Name
                            }).ToList()
                        }
                    },
                    Facts = r.Facts.Select(f => new FactViewModel
                    {
                        Id = f.Id,
                        Name = f.Name
                    }),
                    Exams = r.ExamFile.Select(e => new ExamViewModel
                    {
                        Id = e.Id,
                        Name = e.Name
                    }),
                    Exercises = r.ExerciseFile.Select(e => new ExerciseViewModel
                    {
                        Id = e.Id,
                        Name = e.Name
                    })
                })
            };

            return View(viewModel);
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

        public IActionResult Test()
        {
            var course = _context.Course
                .FirstOrDefault();

            var modules = _moduleRepository.GetModulesForCourse(course.Id); ;

            var viewModel = new CourseViewModel
            {
                Name = course.Name,
                ModulesVM = modules.Select(r => new ModuleViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    Facts = r.Facts.Select(f => new FactViewModel
                    {
                        Id = f.Id,
                        Name = f.Name
                    }),
                    Exams = r.ExamFile.Select(e => new ExamViewModel
                    {
                        Id = e.Id,
                        Name = e.Name
                    }),
                    Exercises = r.ExerciseFile.Select(e => new ExerciseViewModel
                    {
                        Id = e.Id,
                        Name = e.Name
                    })
                })
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
