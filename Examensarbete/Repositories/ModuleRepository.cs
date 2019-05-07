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
                .Where(m => m.FkCourseId == courseId)
                .ToList();

            return modules;
        }

        public Module GetCurrentModule(int moduleId)
        {
            var module = _context.Module
                .SingleOrDefault(m => m.Id == moduleId);

            return module;
        }
    }
}
