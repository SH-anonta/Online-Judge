using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OnlineJudge.FormModels;
using OnlineJudge.Models;
using OnlineJudge.Repository;
using OnlineJudge.ResponseModels;

namespace OnlineJudge.Controllers{
    [RoutePrefix("api/contests")]
    public class ContestController : ApiController{
        private ContestRepository contest_repository;

        public ContestController(){
            contest_repository = new ContestRepository();
        }

        public ContestController(ContestRepository repo){
            contest_repository = repo;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult ContestList(){
            // returns a list of all contests

            var all_contests= contest_repository.GetAllContestsList();
            return Ok(ContestListItem.MapTo(all_contests));
        }


        [HttpGet]
        [Route("")]
        public IHttpActionResult ContestList(int from, int to){
            // returns a list of most recent contests

            var all_contests= contest_repository.GetRecentContestsList(from, to);
            return Ok(ContestListItem.MapTo(all_contests));
        }

        [HttpGet]
        [Route("{contest_id}")]
        public IHttpActionResult ContestDetails(int contest_id){
            try{
                var contest = contest_repository.GetContestById(contest_id);
                return Ok(new ContestDetailsData(contest));
            }
            catch (ObjectNotFoundException e){
                return NotFound();
            }
        }

        [HttpPost]
        [Route("{contest_id}/register")]
        public IHttpActionResult ContestRegistration(int contest_id){
            
            try{
                // todo replace user id 2 with the user sending the request
                contest_repository.RegisterUserForContest(contest_id, 2);
                return Ok();
            }
            catch (ObjectNotFoundException e){
                return NotFound();
            }
            catch (InvalidOperationException e){
                return BadRequest(e.Message);
            }
            
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
            try{
                var contest = contest_repository.GetContestById(contest_id);
                return Ok(new ContestDetailsData(contest));
            }
            catch(ObjectNotFoundException e){
                return NotFound();
            }
        }

        [HttpGet]
        [Route("{contest_id}/problems/{problem_no}")]
        public IHttpActionResult ContestProblemDetails(int contest_id, int problem_no){
            var conest_prblem = contest_repository.GetContestProblem(contest_id, problem_no);
            return Ok(new ContestProblemDetailsData(conest_prblem));
        }

        [HttpPost]
        [Route("{contest_id}/problems/{problem_no}/submit")]
        public IHttpActionResult ContestProblemSubmit(int contest_id, int problem_no, [FromBody] SubmissionFormData submission_data){
            try{
                contest_repository.CreateSubmission(contest_id, problem_no, submission_data);
                return Ok();
            }
            catch (ObjectNotFoundException e){
                return NotFound();
            }
        }

        // submissions
        [HttpGet]
        [Route("{contest_id}/problems/{problem_no}/submissions")]
        public IHttpActionResult ContestProblemSubmissionsResult(int contest_id, int problem_no){
            try{
                var submissions = contest_repository.GetContestantProblemSubmissions(contest_id, problem_no);
                return Ok(SubmissionResponseData.MapTo(submissions));
            }
            catch (ObjectNotFoundException e){
                return NotFound();
            }
        }

        // submissions
        [HttpGet]
        [Route("{contest_id}/submissions")]
        public IHttpActionResult ContestSubmissions(int contest_id){
            try{
                var submissions = contest_repository.GetAllSubmissions(contest_id);
                return Ok(SubmissionResponseData.MapTo(submissions));
            }
            catch (ObjectNotFoundException e){
                return NotFound();
            }
            
        }

        [HttpGet]
        [Route("{contest_id}/contestants")]
        public IHttpActionResult ContestantList(int contest_id){
            try{
                var contestants = contest_repository.GetContestantsOfContest(contest_id);
                return Ok(ContestantListItemData.MapTo(contestants));
            }
            catch(ObjectNotFoundException e){
                return NotFound();
            }
            
        }

        [HttpGet]
        [Route("{contest_id}/contestants/{user_id}/submissions")]
        public IHttpActionResult ContestantSubmissions(int contest_id, int user_id){
            try{
                var submissions = contest_repository.GetContestantSubmissions(contest_id, user_id);
                return Ok(SubmissionResponseData.MapTo(submissions));
            }
            catch (ObjectNotFoundException e){
                return NotFound();
            }
            
        }

    }
}
