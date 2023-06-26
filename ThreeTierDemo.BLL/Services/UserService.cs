using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeTierDemo.DAL.Repositories;
using ThreeTierDemo.DAL;
using ThreeTierDemo.DAL.Entities;
using ThreeTierDemo.BLL.Models;

namespace ThreeTierDemo.BLL.Services
{
    public class UserService
    {
        private readonly UserRepository userDataAccess;
        

        public UserService(string connectionString)
        {

            userDataAccess = new UserRepository(new DBContext(connectionString));
        }

        public void RegisterUser(string username, string email, string password)
        {
           
            userDataAccess.InsertUser(new User {Username=username, Email=email, Password=password});        
          
        }
        public List<UserViewModel> GetAllUsers()
        {
            List<UserViewModel> users = new List<UserViewModel>();

            foreach (var userModel in userDataAccess.GetAllUsers())
            {
                UserViewModel user = new UserViewModel();
                user.ID = userModel.ID;
                user.Username = userModel.Username;
                user.Email = userModel.Email;
                user.Password = userModel.Password;
                users.Add(user);

            }
            return users;
        }
    }
}
