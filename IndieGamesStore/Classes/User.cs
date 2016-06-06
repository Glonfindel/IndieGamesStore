using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C = System.Data.SqlClient;

namespace IndieGamesStore
{
    public class User
    {
        public string Login { get; private set; }
        public string Password { get; private set; }
        public int ID { get; private set; }

        public User()
        {
            
        }
        public User(string login, string password)
        {
            this.Login = login;
            this.Password = password;
        }

        public string Select(string table, string args, string user)
        {
            try
            {
                using (var connection = new C.SqlConnection(
                    "Server=tcp:igs.database.windows.net,1433;Database=IGS_database;User ID=Glonfindel;Password=Karol1995!;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
                    ))
                {
                    connection.Open();
                    C.SqlCommand cmd = new C.SqlCommand
                    {
                        CommandText = "SELECT " + args + " FROM " + table + " WHERE login = '" + user + "'",
                        CommandType = CommandType.Text,
                        Connection = connection
                    };
                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        this.ID = (int) reader["Id"];
                        return reader["password"].ToString();
                    }
                }
                return null;
            }
            catch (C.SqlException)
            {
                return null;
            }
        }
    }
}
