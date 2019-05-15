﻿using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
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
using iTextSharp.text;
using iTextSharp.text.pdf;
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

            var path = "C:\\Users\\Olivia\\Desktop\\Module.1.Facts.pdf";
            var fi = new FileInfo("Module.1.Facts");
            var documentContent = System.IO.File.ReadAllBytes(path);

            //string name = fi.Name;
            //string extn = fi.Extension;

            //TODO: Fixa hårdkodade värden
            var name = "Module.1.Facts";
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

            var path = "C:\\Users\\Olivia\\Desktop\\rainbow.jpg";
            var fi = new FileInfo("rainbow");
            var documentContent = System.IO.File.ReadAllBytes(path);

            //string name = fi.Name;
            //string extn = fi.Extension;

            //TODO: Fixa hårdkodade värden
            var name = "rainbow";
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
