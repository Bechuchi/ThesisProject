using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThesisProject.Models;

namespace ThesisProject.Data
{
    public class FileSeeder
    {
        private ThesisProjectDBContext _context;

        public FileSeeder(ThesisProjectDBContext context)
        {
            _context = context;
        }

        public void SeedDbWithExamFileByLanguage()
        {
            //TODO; Connsträng
            var connectionString = "Server=localhost;Database=ThesisProjectDB;Integrated Security=True;";

            var path = "C:\\Users\\Olivia\\Desktop\\Modul 1_ Prov 1 fr.pdf";
            var fi = new FileInfo("Modul 1_ Prov 1 fr");
            var documentContent = System.IO.File.ReadAllBytes(path);

            //string name = fi.Name;
            //string extn = fi.Extension;

            //TODO: Fixa hårdkodade värden
            var name = "Module.Intro.Exam.1";
            var extn = "pdf";
            var language = "fr";
            var moduleId = 1009;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("SaveExamFile", connection);
                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    command.Parameters.Add("@Name", SqlDbType.VarChar).Value = name;
                    command.Parameters.Add("@Content", SqlDbType.VarBinary, documentContent.Length).Value = documentContent;
                    command.Parameters.Add("@Extn", SqlDbType.VarChar).Value = extn;
                    command.Parameters.Add("@Language", SqlDbType.NChar).Value = language;
                    command.Parameters.Add("@ModuleId", SqlDbType.Int).Value = moduleId;

                    connection.Open();
                    command.ExecuteNonQuery();
                }
                finally
                {
                    //TODO fixa så connection stängs
                    var state = connection.State;
                    connection.Close();
                    state = connection.State;
                }
            }
        }
        //TODO: Fixa connectionsträng kopplat till det här projektet
        //TODO: Stänga connection
        public void SeedDbWithExamFile()
        {
            var connectionString = "Server=localhost;Database=ThesisProjectDB;Integrated Security=True;";

            var path = "C:\\Users\\Olivia\\Desktop\\Olivia_Denbu_LIA-rapport_PROG17.pdf";
            var fi = new FileInfo("Olivia_Denbu_LIA-rapport_PROG17");
            var documentContent = System.IO.File.ReadAllBytes(path);

            //string name = fi.Name;
            //string extn = fi.Extension;

            //TODO: Fixa hårdkodade värden
            var name = "Olivia_Denbu_LIA-rapport_PROG17";
            var extn = "pdf";
            var moduleId = 1009;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("SaveExamFile", connection);
                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    command.Parameters.Add("@Name", SqlDbType.VarChar).Value = name;
                    command.Parameters.Add("@Content", SqlDbType.VarBinary,documentContent.Length).Value = documentContent;
                    command.Parameters.Add("@Extn", SqlDbType.VarChar).Value = extn;
                    command.Parameters.Add("@ModuleId", SqlDbType.Int).Value = moduleId;

                    connection.Open();
                    command.ExecuteNonQuery();
                }
                finally
                {
                    //TODO fixa så connection stängs
                    var state = connection.State;
                    connection.Close();
                    state = connection.State;
                }
            }
        }

        public void SeedDbWithExerciseFile()
        {
            var connectionString = "Server=localhost;Database=ThesisProjectDB;Integrated Security=True;";

            var path = "C:\\Users\\Olivia\\Desktop\\Frågesport 1.pdf";
            var fi = new FileInfo("Frågesport 1");
            var documentContent = System.IO.File.ReadAllBytes(path);

            //string name = fi.Name;
            //string extn = fi.Extension;

            //TODO: Fixa hårdkodade värden
            var name = "Frågesport 1";
            var extn = "pdf";
            var moduleId = 1009;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("SaveExerciseFile", connection);
                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    command.Parameters.Add("@Name", SqlDbType.VarChar).Value = name;
                    command.Parameters.Add("@Content", SqlDbType.VarBinary, documentContent.Length).Value = documentContent;
                    command.Parameters.Add("@Extn", SqlDbType.VarChar).Value = extn;
                    command.Parameters.Add("@ModuleId", SqlDbType.Int).Value = moduleId;

                    connection.Open();
                    command.ExecuteNonQuery();
                }
                finally
                {
                    //TODO fixa så connection stängs
                    var state = connection.State;
                    connection.Close();
                    state = connection.State;
                }
            }
        }

        public void SeedDbWithFactsFile()
        {
            var connectionString = "Server=localhost;Database=ThesisProjectDB;Integrated Security=True;";

            var path = "C:\\Users\\Olivia\\Desktop\\Module.1.Fact.pdf";
            var fi = new FileInfo("Module.1.Fact");
            var documentContent = System.IO.File.ReadAllBytes(path);

            //string name = fi.Name;
            //string extn = fi.Extension;

            //TODO: Fixa hårdkodade värden
            var name = "Module.1.Fact";
            var extn = "pdf";
            var moduleId = 1009;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("SaveFactsFile", connection);
                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    command.Parameters.Add("@Name", SqlDbType.VarChar).Value = name;
                    command.Parameters.Add("@Content", SqlDbType.VarBinary, documentContent.Length).Value = documentContent;
                    command.Parameters.Add("@Extn", SqlDbType.VarChar).Value = extn;
                    command.Parameters.Add("@ModuleId", SqlDbType.Int).Value = moduleId;

                    connection.Open();
                    command.ExecuteNonQuery();
                }
                finally
                {
                    //TODO fixa så connection stängs
                    var state = connection.State;
                    connection.Close();
                    state = connection.State;
                }
            }
        }

        public void SeedDbWithImage()
        {
            var connectionString = "Server=localhost;Database=ThesisProjectDB;Integrated Security=True;";

            var path = "C:\\Users\\Olivia\\Desktop\\Alla.Höra.jpg";
            var fi = new FileInfo("Alla.Höra");
            var documentContent = System.IO.File.ReadAllBytes(path);

            //string name = fi.Name;
            //string extn = fi.Extension;

            //TODO: Fixa hårdkodade värden
            var name = "if-music";
            var extn = "jpg";
            var moduleId = 1009;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("SaveImage", connection);
                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    command.Parameters.Add("@Name", SqlDbType.VarChar).Value = name;
                    command.Parameters.Add("@Content", SqlDbType.VarBinary, documentContent.Length).Value = documentContent;
                    command.Parameters.Add("@Extn", SqlDbType.VarChar).Value = extn;
                    command.Parameters.Add("@ModuleId", SqlDbType.Int).Value = moduleId;

                    connection.Open();
                    command.ExecuteNonQuery();
                }
                finally
                {
                    //TODO fixa så connection stängs
                    var state = connection.State;
                    connection.Close();
                    state = connection.State;
                }
            }
        }

    }
}
