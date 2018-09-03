using OnlineJudge.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineJudge.Controllers{
    

    [RoutePrefix("api/auth-check")]
    public class AuthCheckController : ApiController{
        private UserService user_service = new UserService();

        [HttpGet]
        [Route("edit-contest")]
        public IHttpActionResult CanEditProblem(int contest_id){
            return Ok(user_service.IsAuthorizedToEditContest(contest_id));
        }

        [HttpGet]
        [Route("submit-contest")]
        public IHttpActionResult CanSubmitToContest(int contest_id){
            return Ok(user_service.IsAuthorizedToSubmitToContest(contest_id));
        }
    }
}
