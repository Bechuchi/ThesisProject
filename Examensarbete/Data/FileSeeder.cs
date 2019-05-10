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

        //TODO Fixa så filen går att öppna 
        //Laddar nu ner filen men öppnar ej
        public byte[] GetFile()
        {
            var connectionString = "Server=localhost;Database=ThesisProjectDB;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("GetExamFileById", connection);
                connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = 12; //TODO fixa värdena (id för lagrade pdf)

                byte[] myBytes = new byte[0];
                SqlDataReader reader = cmd.ExecuteReader();

                reader.Read();
                myBytes = (byte[])reader["Content"];
                return myBytes;

                //var fileName = reader["Name"].ToString();
                //var extn = reader["extn"].ToString();
                //var path = "C:\\Users\\" + fileName + "." + extn;


                ////TODO: Byt ut C: till path
                //using (StreamWriter stream = new StreamWriter("C:\\Users\\Olivia\\Desktop\\download.pdf"))
                //{
                //    BinaryWriter bw = new BinaryWriter(stream.BaseStream);
                //    bw.Write(myBytes);

                //}

                //return myBytes;
            }
        }

            public void Download()
        {
            var connectionString = "Server=localhost;Database=ThesisProjectDB;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("GetExamFileById", connection);
                connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = 12; //TODO fixa värdena (id för lagrade pdf)

                byte[] myBytes = new byte[0];
                SqlDataReader reader = cmd.ExecuteReader();

                reader.Read();
                myBytes = (byte[])reader["Content"];

                var fileName = reader["Name"].ToString();
                var extn = reader["extn"].ToString();
                var path = "C:\\Users\\" + fileName + "." + extn;


                //TODO: Byt ut C: till path
                using (StreamWriter stream = new StreamWriter("C:\\Users\\Olivia\\Desktop\\download.pdf"))
                {
                    BinaryWriter bw = new BinaryWriter(stream.BaseStream);
                    bw.Write(myBytes);

                }
               
                //return myBytes;
            }
        }
    }
}
