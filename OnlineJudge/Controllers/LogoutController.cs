using System.Web.Http;
using OnlineJudge.Services;

namespace OnlineJudge.Controllers{

    [RoutePrefix("api/logout")]
    public class LogoutController : ApiController{

        private UserService user_service;

        public LogoutController(){
            this.user_service = new UserService();
        }


        [HttpPost]
        public IHttpActionResult Logout(){
            user_service.LogoutUser();
            return Ok();
        }
    }
}
