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

        public FileRepository(ThesisProjectDBContext context)
        {
            _context = context;
        }

        public byte[] GetExamFile(int id)
        {
            id = 14;
            var connectionString = "Server=localhost;Database=ThesisProjectDB;Integrated Security=True;";

            using (var connection = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("GetExamFileById", connection);
                connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id; //TODO fixa värdena (id för lagrade pdf)

                byte[] myBytes = new byte[0];

                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                myBytes = (byte[])reader["Content"];

                return myBytes;
            }
        }

        public byte[] GetCurrentFile(int fileId, string cmdText)
        {
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

    }
}
