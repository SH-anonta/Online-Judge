using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OnlineJudge.Models;

namespace OnlineJudge.Controllers
{
    [RoutePrefix("api/problems")]
    public class ProblemController : ApiController{

        [Route("submit")]
        [HttpPost]
        public string Submit(SubmissionRequestData data){
            return data.SolutionCode;
        }
    }
}
