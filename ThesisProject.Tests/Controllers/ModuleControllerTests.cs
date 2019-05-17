using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using ThesisProject.Controllers;
using Xunit;

namespace ThesisProject.Tests.Controllers
{
    public class ModuleControllerTests
    {
        public void BrowsePdfShouldReturnMimeApplicationPdf(int fileId, string pdfType)
        {
            //Moqa context(?) TODO
            var controller = new ModulesController();

        var result = controller.Details(2, "facts") as ViewResult;
        Assert.AreEqual("Details", result.ViewName);
        }

        public ActionResult BrowsePdf(int fileId, string pdfType)
        {
            string cmdText = "";

            switch (pdfType)
            {
                case "facts":
                    cmdText = "GetFactsFileById";
                    break;
                case "exercises":
                    cmdText = "GetExerciseFileById";
                    break;
                case "exams":
                    cmdText = "GetExamFileById";
                    break;
                default:
                    break;
            }

            var file = _fileRepository.GetCurrentFile(fileId, cmdText);

            return new FileContentResult(file, "application/pdf");
        }
    }
}
