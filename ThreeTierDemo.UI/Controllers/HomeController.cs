using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThreeTierDemo.BLL.Models;
using ThreeTierDemo.BLL.Services;


namespace ThreeTierDemo.UI.Controllers
{
     
    public class HomeController : Controller
    {
        static readonly string connString = ConfigurationManager.ConnectionStrings["myDBConnection"].ConnectionString;

        public UserService UserService = new UserService(connString);
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveUser(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
               
                UserService.RegisterUser(user.Username,user.Email,user.Password);
                // Redirect to the ListUsers action method
                return RedirectToAction("ListUsers");
            }

            // If ModelState is not valid, return the view with validation errors
            ViewBag.User = true;
            return View("Index");
        }

        public ActionResult ListUsers()
        {
            List<UserViewModel> userList = UserService.GetAllUsers();


            return View(userList);
        }
    }
}