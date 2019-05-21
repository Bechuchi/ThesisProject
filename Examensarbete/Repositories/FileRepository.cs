using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ThesisProject.Models;

namespace ThesisProject.Repositories
{
    public class FileRepository
    {
        private readonly ThesisProjectDBContext _context;
        private readonly IConfiguration _configuration;
        private string _currentLanguage;

        public FileRepository(ThesisProjectDBContext context,
                              IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public byte[] GetExamFile(int id)
        {
            //TODO ta bort hårdkodat id
            id = 14;
            //var connectionString = "Server=localhost;Database=ThesisProjectDB;Integrated Security=True;";
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (var connection = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("GetExamFileById", connection);
                connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id; //TODO fixa värdena (id för lagrade pdf)
                //cmd.Parameters.Add("@Language", SqlDbType.Int).Value = language;

                byte[] myBytes = new byte[0];

                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                myBytes = (byte[])reader["Content"];

                return myBytes;
            }
        }

        public byte[] GetCurrentFile(int fileId, string cmdText)
        {
            //TODO fixa connsträng
            var connectionString = "Server=localhost;Database=ThesisProjectDB;Integrated Security=True;";

            using (var connection = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand(cmdText, connection);
                connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = fileId; //TODO fixa värdena (id för lagrade pdf)

                byte[] myBytes = new byte[0];

                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                myBytes = (byte[])reader["Content"];

                return myBytes;
            }
        }

        public byte[] GetFileToDownload(int fileId, string pdfType)
        {
            var file = new byte[0];

            switch (pdfType)
            {
                case "facts":
                    file = _context.Facts
                        .Where(f => f.Id == fileId)
                        .Select(f => f.Content)
                        .SingleOrDefault();
                    break;
                case "exercises":
                    file = _context.ExerciseFile
                        .Where(f => f.Id == fileId)
                        .Select(f => f.Content)
                        .SingleOrDefault();
                    break;
                case "exams":
                    file = _context.ExamFile
                        .Where(f => f.Id == fileId)
                        .Select(f => f.Content)
                        .SingleOrDefault();
                    break;
                default:
                    break;
            }

            return file;
        }
    }
}
