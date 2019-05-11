using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThesisProject.Models;

namespace ThesisProject.ViewModels
{
    public class ModuleViewModel
    {
        public string Name { get; set; }
        public IEnumerable<Facts> Facts { get; set; }
        public IEnumerable<ExerciseFile> Exercises { get; set; }
        public IEnumerable<ExamFile> Exams { get; set; }
    }
}
