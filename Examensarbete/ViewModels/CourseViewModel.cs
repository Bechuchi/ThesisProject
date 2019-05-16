using ThesisProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThesisProject.ViewModels
{
    public class CourseViewModel
    {
        public string Name { get; set; }
        public IEnumerable<Module> Modules { get; set; }
        public IEnumerable<ModuleViewModel> ModulesVM { get; set; }
        public ModuleViewModel CurrentDisplayModule { get; set; } //TODO Ta bort
    }
}
