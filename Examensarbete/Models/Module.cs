﻿using System;
using System.Collections.Generic;

namespace ThesisProject.Models
{
    public partial class Module
    {
        public Module()
        {
            ExamFile = new HashSet<ExamFile>();
            ExerciseFile = new HashSet<ExerciseFile>();
            Facts = new HashSet<Facts>();
            Image = new HashSet<Image>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public int? CourseId { get; set; }

        public Course Course { get; set; }
        public ICollection<ExamFile> ExamFile { get; set; }
        public ICollection<ExerciseFile> ExerciseFile { get; set; }
        public ICollection<Facts> Facts { get; set; }
        public ICollection<Image> Image { get; set; }
    }
}
