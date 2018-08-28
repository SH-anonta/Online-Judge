using OnlineJudge.Models;
using OnlineJudge.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace OnlineJudge.Controllers
{
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        private UserRepository user_repository;

        public LoginController()
        {
            this.user_repository = new UserRepository();
        }

        //GET: api/login
        public void GetLogin()
        {
            
        }


        // POST: api/Login
        public void Post(Login credentials)
        {
            Login loginDetails = new Login (){
                Username = credentials.Username,
                Password = credentials.Password
            };

            if(ModelState.IsValid)
            {
                if (user_repository.IsValidUser(loginDetails.Username, loginDetails.Password))
                {
                    HttpContext.Current.Session["userName"] = loginDetails.Username;
                    RedirectToRoute("api/announcements", 200);
                }
                else
                {

                }
            }
            
        }
    }
}
