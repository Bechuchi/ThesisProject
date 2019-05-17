using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using ThesisProject.Controllers;
using ThesisProject.Models;

namespace ThesisProjectTests.Controllers
{
    [TestFixture]
    class ModulesControllerTest
    {
        private ModulesController _controller;
        private ThesisProjectDBContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = new ThesisProjectDBContext();
            _controller = new ModulesController(_context, null);
        }

    }
}
