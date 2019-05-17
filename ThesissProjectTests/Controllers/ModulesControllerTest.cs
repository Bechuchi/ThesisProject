using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ThesisProject.Controllers;
using ThesisProject.Models;
using ThesisProject.ViewModels;
using Xunit;

namespace ThesissProjectTests.Controllers
{
    public class ModulesControllerTest
    {
        ThesisProjectDBContext _context;

        [Fact]
        public void Test1()
        {
            var options = new DbContextOptionsBuilder<ThesisProjectDBContext>()
                .UseInMemoryDatabase()
                .Options;

            _context = new ThesisProjectDBContext(options);

            var controller = new HomeController(_context, null);
            

            var result = controller.Index();
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<IndexViewModel>(viewResult.ViewData.Model);
            Assert.Equal("Welcome", model.Heading);
        }
    }
}
