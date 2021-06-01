using MySql.Data.MySqlClient;
using Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DAO
{
    class UserDAO
    {
        MySqlDataReader reader;
        public User VerifyUser(MySqlConnection conn,string username,string password)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from user where username =@username and password = @password",conn);

                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("password", password);

                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int id = reader.GetInt32("id");
                    User user = new User(id, username, password);
                    return user;
                }
                else return null;
            }
            catch (Exception e )
            {
                Console.WriteLine(e);
            }
            finally
            {
                reader.Close();
            }
            return null;
        }
    }
}
