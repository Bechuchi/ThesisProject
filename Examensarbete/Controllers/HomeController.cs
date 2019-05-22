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
using Microsoft.Extensions.Configuration;

namespace ThesisProject.Controllers
{
    public class HomeController : Controller
    {
        public string CurrentLanguage { get; set; }
        private ThesisProjectDBContext _context;
        private ModuleRepository _moduleRepository;
        private IConfiguration _configuration;
        private readonly IStringLocalizer<HomeController> _localizer;


        public HomeController(ThesisProjectDBContext context,
                              IStringLocalizer<HomeController> localizer,
                              IConfiguration configuration)
        {
            _context = context;
            //TODO dependendy injecton
            _moduleRepository = new ModuleRepository(_context);
            _localizer = localizer;
            _configuration = configuration;
        }

        //public IActionResult Today()
        //{
        //    var course = _context.Course
        //        .FirstOrDefault();

        //    var modules = _moduleRepository.GetModulesForCourse(course.Id); ;

        //    var viewModel = new CourseViewModel
        //    {
        //        Name = course.Name,
        //        ModulesVM = modules.Select(r => new ModuleViewModel
        //        {
        //            Id = r.Id,
        //            Name = r.Name,
        //            PartsOfModule = new List<PartOfModule>()
        //            {
        //                new PartOfModule
        //                {
        //                    Id = 1,
        //                    PartType = "facts",
        //                    FactsNew = r.Facts.Select(f => new FactViewModel
        //                    {
        //                        Id = f.Id,
        //                        Name = f.Name
        //                    }).ToList()
        //                },
        //                new PartOfModule
        //                {
        //                    Id = 2,
        //                    PartType = "exercises"
        //                },
        //                new PartOfModule
        //                {
        //                    Id = 3,
        //                    PartType = "exams",
        //                    ExamsNew = r.ExamFile.Select(e => new ExamViewModel
        //                    {
        //                        Id = e.Id,
        //                        Name = e.Name
        //                    }).ToList()
        //                }
        //            },
        //            Facts = r.Facts.Select(f => new FactViewModel
        //            {
        //                Id = f.Id,
        //                Name = f.Name
        //            }),
        //            Exams = r.ExamFile.Select(e => new ExamViewModel
        //            {
        //                Id = e.Id,
        //                Name = e.Name
        //            }),
        //            Exercises = r.ExerciseFile.Select(e => new ExerciseViewModel
        //            {
        //                Id = e.Id,
        //                Name = e.Name
        //            })
        //        })
        //    };

        //    return View(viewModel);
        //}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Course()
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
                    }),
                    Images = r.Image.Select(e => new ImageViewModel
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

            CurrentLanguage = culture;

            return LocalRedirect(returnUrl);
        }
    }
}
