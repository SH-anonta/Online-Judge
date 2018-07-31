using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineJudge.Controllers{
    [RoutePrefix("contests")]
    public class ContestController : ApiController{
        
        [HttpGet]
        [Route("")]
        public IHttpActionResult ContestList(){
            // todo implement
            return Ok();
        }

        [HttpGet]
        [Route("{contest_id}")]
        public IHttpActionResult ContestDetails(int contest_id){
            // todo implement
            return Ok();
        }

        [HttpPost]
        [Route("{contest_id}/register")]
        public IHttpActionResult ContestRegistration(int contest_id){
            // todo implement
            return Ok();
        }

        [HttpPost]
        [Route("create")]
        public IHttpActionResult ContestCreate(){
            // todo implement
            return Ok();
        }

        [HttpPost]
        [Route("{contest_id}/edit")]
        public IHttpActionResult ContestUpdate(int contest_id){
            // todo implement
            return Ok();
        }

        // contest problems
        [HttpGet]
        [Route("{contest_id}/problems")]
        public IHttpActionResult ContestProblemList(int contest_id){
            // todo implement
            return Ok();
        }

        [HttpGet]
        [Route("{contest_id}/problems/{problem_no}")]
        public IHttpActionResult ContestProblemDetails(int contest_id, string problem_no){
            // todo implement
            return Ok();
        }

        [HttpPost]
        [Route("{contest_id}/problems/{problem_no}/submit")]
        public IHttpActionResult ContestProblemSubmit(int contest_id, string problem_no){
            // todo implement
            return Ok();
        }

        // submissions
        [HttpGet]
        [Route("{contest_id}/problems/{problem_no}/submissions")]
        public IHttpActionResult ContestProblemSubmissionsResult(int contest_id, string problem_no){
            // todo implement
            return Ok();
        }

        [HttpGet]
        [Route("{contest_id}/user/{user_id}/submissions")]
        public IHttpActionResult ContestantSubmissions(int contest_id, int user_id){
            // todo implement
            return Ok();
        }

    }
}
