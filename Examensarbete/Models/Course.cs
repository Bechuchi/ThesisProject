using System;
using System.Collections.Generic;

namespace ThesisProject.Models
{
    public partial class Course
    {
        public Course()
        {
            Module = new HashSet<Module>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }

        public ICollection<Module> Module { get; set; }
    }
}
