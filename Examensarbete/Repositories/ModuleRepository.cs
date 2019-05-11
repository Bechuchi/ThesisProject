using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThesisProject.Models;

namespace ThesisProject.Repositories
{
    public class ModuleRepository
    {
        private readonly ThesisProjectDBContext _context;

        public ModuleRepository(ThesisProjectDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Module> GetModulesForCourse(int courseId)
        {
            var modules = _context.Module
                .Include(e => e.ExamFile)
                .Where(m => m.CourseId == courseId)
                .ToList();

            return modules;
        }

        public Module Get(int moduleId)
        {
            var module = _context.Module
                .Include(e => e.ExamFile)
                .Include(e => e.ExerciseFile)
                .Include(f => f.Facts)
                .SingleOrDefault(m => m.Id == moduleId);

            return module;
        }
    }
}
