using OnlineJudge.Models;
 using System.Web.Http;
using OnlineJudge.Services;

namespace OnlineJudge.Controllers
{
    [RoutePrefix("api/login")]
    public class LoginController : ApiController{
        private UserService user_service;

        public LoginController(){
            
        }
        
        [HttpPost]
        [HttpOptions]
        [Route("")]
        public IHttpActionResult Login(Login credentials){
            this.user_service = new UserService();

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
