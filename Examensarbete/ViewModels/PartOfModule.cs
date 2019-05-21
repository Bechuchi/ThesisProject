using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThesisProject.ViewModels
{
    public class PartOfModule
    {
        public PartOfModule()
        {
            FactsNew = new List<FactViewModel>();
            ExercisesNew = new List<ExerciseViewModel>();
            ExamsNew = new List<ExamViewModel>();
            MyCollection = new List<FileViewModel>();
        }

        public int Id { get; set; }
        public string PartType { get; set; }

        public List<FileViewModel> MyCollection { get; set; }
        public List<FactViewModel> FactsNew { get; set; }
        public List<ExerciseViewModel> ExercisesNew { get; set; }
        public List<ExamViewModel> ExamsNew { get; set; }
    }
}
