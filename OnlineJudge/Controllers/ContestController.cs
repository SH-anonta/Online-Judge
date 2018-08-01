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
            try
            {
                var contestant = contest_repository.RegisterUserForContest(contest_id, 1);
                return Ok(contestant.Contest.Title);
            }
            catch (ObjectNotFoundException e)
            {
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
