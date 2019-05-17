using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using ThesisProject.Controllers;
using ThesisProject.Models;

namespace ThesisProjecttTests.Controllers
{ 
    [TestClass]
    public class ModulesControllerTest
    {
        ThesisProjectDBContext _context;

        [TestMethod]
        public void Test()
        {
            var optionBuilder = new DbContextOptionsBuilder<ThesisProjectDBContext>();
            optionBuilder.UseInMemoryDatabase();

            _context = new ThesisProjectDBContext(optionBuilder.Options);

            var controller = new ModulesController(_context);
        }

    }
}
