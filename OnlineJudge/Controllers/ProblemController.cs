using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OnlineJudge.Models;
using OnlineJudge.Repository;
using OnlineJudge.ResponseModels;
using OnlineJudge.Services;

namespace OnlineJudge.Controllers{
    [RoutePrefix("api/problems")]
    public class ProblemController : ApiController{
        private static readonly log4net.ILog logger =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        
        // return all problems in order of creation date
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetProblemList(){
            return Ok(DataRepository.GetProblemList());
        }

        [Route("{Id}")]
        [HttpGet]
        public IHttpActionResult GetProblemDetials(int Id){
            try{
                ProblemDetails problem = DataRepository.GetProblemDetails(Id);
                return Ok(problem);
            }
            catch (ObjectNotFoundException e){
                return NotFound();
            }
            
                

            
        }
      
        [Route("submit")]
        [HttpPost]
        public IHttpActionResult Submit(SubmissionRequestData data){
            var judge = new JudgeService();

            try{
                judge.judge(data);
            }
            catch (Exception e){
                logger.Error("Failed to judge submission", e);
            }
            
            return Ok();
        }
    }


    [RoutePrefix("api/all-submissions")]
    public class SubmissionsController : ApiController{

        [Route("")]
        [HttpGet]
        public Object AllSubmissions(SubmissionRequestData data){
            var ctx = new OjDBContext();
            var subs = ctx.Submissions.Where(x => true);
            return SubmissionResponseData.MapTo(subs);
        }
    }
}
