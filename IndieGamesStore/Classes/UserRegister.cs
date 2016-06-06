using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using C = System.Data.SqlClient;
using IndieGamesStore.Interfaces;

namespace IndieGamesStore
{
    class UserRegister : User, IInsert
    {
        public string PasswordRepeat { get; private set; }
        public string Email { get; private set; }
        public string EmailRepeat { get; private set; }

        public UserRegister()
        {
        }

        public UserRegister(string login, string password, string passwordRepeat, string email, string emailRepeat) : base(login, password)
        {
            this.PasswordRepeat = passwordRepeat;
            this.Email = email;
            this.EmailRepeat = emailRepeat;
        }

        public void Insert(string table, object[] args)
        {
            try
            {
                using (var connection = new C.SqlConnection(
                    "Server=tcp:igs.database.windows.net,1433;Database=IGS_database;User ID=Glonfindel;Password=Karol1995!;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
                    ))
                {
                    connection.Open();
                    using (C.SqlCommand cmd = new C.SqlCommand("INSERT INTO " + table + "(login, password, email) VALUES (@login, @password, @email)"))
                    {
                        cmd.Connection = connection;
                        cmd.Parameters.Add("@login", SqlDbType.VarChar, 32).Value = args[0];
                        cmd.Parameters.Add("@password", SqlDbType.VarChar, 255).Value = args[1];
                        cmd.Parameters.Add("@email", SqlDbType.VarChar, 255).Value = args[2];
                        cmd.ExecuteNonQuery();
                    };

                }
            }
            catch
                (C.SqlException)
            {
                MessageBox.Show("Failed!");
            }
        }
    }
}
