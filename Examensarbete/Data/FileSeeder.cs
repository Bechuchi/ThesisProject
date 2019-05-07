using iTextSharp.text.pdf;
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

        public string GetTextFromPDF()
        {
            //TODO: try catch
            StringBuilder text = new StringBuilder();
            var path = "C:\\Users\\Olivia\\Desktop\\Olivia_Denbu_LIA-rapport_PROG17.pdf";
            //path = "C:\\Users\\Olivia\\Desktop\\TestFakta.pdf";

            using (PdfReader reader = new PdfReader(path))
            {
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                }
            }

            return text.ToString();
        }

        public void SeedFile()
        {
            var connectionString = "Server=localhost;Database=ThesisProjectDB;Integrated Security=True;";

            var path = "C:\\Users\\Olivia\\Desktop\\Olivia_Denbu_LIA-rapport_PROG17.pdf";
            var fi = new FileInfo("Olivia_Denbu_LIA-rapport_PROG17");
            var documentContent = GetBytesFromFile(path);

            //string name = fi.Name;
            //string extn = fi.Extension;

            var name = "Olivia_Denbu_LIA-rapport_PROG17";
            var extn = "pdf";
            var moduleId = 1;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("SaveExamFile", connection);
                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    command.Parameters.Add("@Name", SqlDbType.VarChar).Value = name;
                    command.Parameters.Add("@Content", SqlDbType.VarBinary).Value = documentContent;
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
      
        public static byte[] GetBytesFromFile(string path)
        {
            // this method is limited to 2^32 byte files (4.2 GB)
            var bytes = System.IO.File.ReadAllBytes(path);

            return bytes;
        }

        public byte[] Download()
        {
            var connectionString = "Server=localhost;Database=ThesisProjectDB;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("GetExamFileById", connection);
                connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = 1;

                byte[] myBytes = new byte[0];
                SqlDataReader reader = cmd.ExecuteReader();

                reader.Read();
                myBytes = (byte[])reader["Content"];

                var fileName = reader["Name"].ToString();
                var extn = reader["extn"].ToString();
                var path = "C:\\Users\\" + fileName + "." + extn;


                //TODO: Byt ut C: till path
                using (StreamWriter stream = new StreamWriter("C:\\Users\\Olivia\\Desktop\\testpdf.pdf"))
                {
                    BinaryWriter bw = new BinaryWriter(stream.BaseStream);
                    bw.Write(myBytes);
                    //bw.Close();
                    //System.IO.File.Delete(file);

                    //FileStreamResult fileStreamResult = new FileStreamResult("application/pdf");
                    //FileStreamResult fileStreamResult = new FileStreamResult()

                    //fileStreamResult.FileDownloadName = "Sample.pdf";
                }
                return myBytes;
            }
        }
    }
}
