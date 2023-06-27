using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeTierDemo.DAL.Entities;

namespace ThreeTierDemo.DAL.Repositories
{
    public class UserRepository
    {
   
            private readonly string connectionString;

            public UserRepository()
            {
            connectionString = ConfigurationManager.ConnectionStrings["myDBConnection"].ConnectionString; 
            }

            public void InsertUser(User user)
            {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "InsertNewUser";

                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.ExecuteNonQuery();
                connection.Close();


            }
            }

            public List<User> GetAllUsers()
            {
                List<User> users = new List<User>();
                DataTable dt = new DataTable();

                using (var connection = new SqlConnection(connectionString))
                {
                 
                    connection.Open();

                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GetAllUsers";
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {
                        User user = new User
                        {
                            ID = (int)dr[0],
                            Username = dr[1].ToString(),
                            Email = dr[2].ToString(),
                            Password = dr[3].ToString()
                        };
                        users.Add(user);
                    }
                    connection.Close();
                }

                return users;
            }

        }
    }

