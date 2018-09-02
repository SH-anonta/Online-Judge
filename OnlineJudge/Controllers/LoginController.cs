using OnlineJudge.Models;
using OnlineJudge.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using OnlineJudge.Services;

namespace OnlineJudge.Controllers
{
    [RoutePrefix("api/login")]
    public class LoginController : ApiController{
        private UserService user_service;

        public LoginController(){
            this.user_service = new UserService();
        }

        //GET: api/login
        public void GetLogin(){}


        [HttpPost]
        [HttpOptions]
        [Route("")]
        public IHttpActionResult Login(Login credentials){
            if (RequestUtility.IsPreFlightRequest(Request)){
                return Ok();
            }

            bool success = user_service.LoginUser(credentials.Username, credentials.Password);

            if (success){
                return Ok();
            }
            else{
                return BadRequest();
            }
        }

    }
}
