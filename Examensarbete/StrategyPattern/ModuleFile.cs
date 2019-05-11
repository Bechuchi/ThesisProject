using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ThesisProject.StrategyPattern
{
    public class ModuleFile : IDownloadFileBehaviour
    {
        public void Download(int fileId, string storedProcedure)
        {
            var connectionString = "Server=localhost;Database=ThesisProjectDB;Integrated Security=True;";

            using (var connection = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand(storedProcedure, connection);

                connection.Open();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = fileId; //TODO fixa värdena (id för lagrade pdf)

                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                var bytes = new byte[0];
                bytes = (byte[])reader["Content"];

                //TODO: Byt ut C: till path
                using (var stream = new StreamWriter("C:\\Users\\Olivia\\Desktop\\download.pdf"))
                {
                    var bw = new BinaryWriter(stream.BaseStream);
                    bw.Write(bytes);
                }
                //return RedirectToAction("Index", "Home");
            }
        }
    }
}
