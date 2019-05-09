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

        [HttpPost]
        public IActionResult Details(int id)
        {
            var module = _moduleRepository.GetModule(id);
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