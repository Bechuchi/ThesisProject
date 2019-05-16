using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ThesisProject.Models;

namespace ThesisProject.ViewModels
{
    public class ModuleViewModel
    {
        public string pdfType { get; set; }

        public string Name { get; set; }
        public IEnumerable<Facts> Facts { get; set; }
        public IEnumerable<ExerciseFile> Exercises { get; set; }
        public IEnumerable<ExamFile> Exams { get; set; }
    }
}
