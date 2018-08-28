using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineJudge.Models;

namespace OnlineJudge.Controllers{


    [RoutePrefix("admin-site")]
    public class AdminController : Controller{
        [HttpGet]
        [Route("index")]
        public ActionResult Index(){
            return View("AdminHome");
        }

        

         
        //user profile
        [HttpGet]
        [Route("user")]
        public ActionResult UserProfile()
        {
            UserTest usertest = new UserTest { Id = 1, UserName = "Saif", Title = "Mohhmad", Email = "ezaz@gmail.com", Password = "abcd" , Usertype = "Admin" };

            return View("User",usertest);
        }

        UserTest[] usertestList = new UserTest[]
            {
                new UserTest { Id = 1, UserName = "Saif", Title = "Mohhmad", Email = "ezaz@gmail.com", Password = "abcd", Usertype = "Admin" },
                new UserTest { Id = 2, UserName = "kaif", Title = "Mohhmad", Email = "kaif@gmail.com", Password = "abcd", Usertype = "User" },
                new UserTest { Id = 3, UserName = "taif", Title = "Mohhmad", Email = "taif@gmail.com", Password = "abcd", Usertype = "Admin" }, 
            };

        [HttpGet]
        [Route("Edit")]
        public ActionResult EditUserProfile(int id)
        {
            UserTest usertest = usertestList.Where(u => u.Id == id).SingleOrDefault();
            return View("EditProfile", usertest);
        }


    }
}