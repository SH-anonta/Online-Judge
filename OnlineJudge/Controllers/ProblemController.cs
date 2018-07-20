using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OnlineJudge.Models;
using OnlineJudge.Services;

namespace OnlineJudge.Controllers
{
    [RoutePrefix("api/problems")]
    public class ProblemController : ApiController{

        [Route("submit")]
        [HttpPost]
        public string Submit(SubmissionRequestData data)
        {
            var judge = new JudgeService();
            judge.judge(data);
            
            return data.SolutionCode;
        }
    }

    [RoutePrefix("api/all-submissions")]
    public class SubmissionsController : ApiController{

        [Route("")]
        [HttpGet]
        public Object AllSubmissions(SubmissionRequestData data){
            var ctx = new OjDBContext();
            return ctx.Submissions.Where((x) => true);
        }
    }
}
