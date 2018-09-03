using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineJudge.Models;
using OnlineJudge.Repository;
using OnlineJudge.Services;

namespace OnlineJudge.Controllers{


    [RoutePrefix("admin-site")]
    public class AdminController : Controller{
        private UserService user_service = new UserService();           // use this to log in/out the user and do authorization checks
        private UserRepository user_repository = new UserRepository();     // use this to get user data from db
        

        [HttpGet]
        [Route("")]
        public ActionResult Index(){
            return View("AdminHome");
        }
         
        //user profile

        [HttpGet]
        [Route("users")]
        public ActionResult UserList(){
            // todo: show list of uesrs 
            return null;
        }

        [HttpGet]
        [Route("users/{user_id}")]
        public ActionResult UserProfile() {
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
        [Route("users/{user_id}/edit")]
        public ActionResult EditUserProfile(int user_id){
            UserTest usertest = usertestList.Where(u => u.Id == user_id).SingleOrDefault();
            return View("EditProfile", usertest); 
        }

        [HttpPost]
        [Route("users/{user_id}/edit")]
        public ActionResult EditUserProfilePost(int user_id){
            // todo handle user prfile edit form submission
            return null;
        }


    }
}