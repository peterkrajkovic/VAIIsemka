using Microsoft.Data.SqlClient;
using System.Data;
using System.Dynamic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Intrinsics.Arm;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ServerApi.Database
{
    public class General
    {
        private static General instance;
        private string connectionString;

        private General()
        {
            var configuration = new ConfigurationBuilder()
                    .AddJsonFile("Database/appsettings.Database.json")
                    .Build();

            // Retrieve the connection string
            connectionString = (string)configuration.GetConnectionString("DefaultConnection")!.Trim();
        }

        // Public method to access the singleton instance
        public static General GetInstance
        {
            get
            {
                // Double-check locking for thread safety
                if (instance == null)
                {
                    lock (new object())
                    {
                        if (instance == null)
                        {
                            instance = new General();
                        }
                    }
                }

                return instance;
            }
        }
        

        internal List<dynamic> CallProcedure(string procName,ref Dictionary<string, string> parameters)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(procName, connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    foreach (var parameter in parameters)
                    {
                        cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
                    }
                    var result = new List<dynamic>();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var item = new ExpandoObject() as IDictionary<string, object>;

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                item.Add(reader.GetName(i), reader.GetValue(i));
                            }

                            result.Add(item);
                        }
                    }
                    return result;
                }
            }
            catch (Exception e)
            {
                var ex = e.Message;
                return null;
            }
        }
       
    }
}
