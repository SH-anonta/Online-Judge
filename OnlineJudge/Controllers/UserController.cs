using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using OnlineJudge.FormModels;
using OnlineJudge.Repository;
using OnlineJudge.ResponseModels;

namespace OnlineJudge.Controllers{
    [RoutePrefix("api/users")]
    public class UserController : ApiController{
        public UserRepository user_repository= new UserRepository();

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult UserDetails(int id){

            try{
                return Ok(new UserDetailsData(user_repository.GetUserDetails(id)));
            }
            catch (ObjectNotFoundException e){
                return NotFound();
            }
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult UserList(){
            return Ok(UserListItemData.MapTo(user_repository.GetUserList()));
        }

        [HttpPost]
        [Route("{id}/edit")]
        public IHttpActionResult UpdateUser([FromBody]UserProfileUpdateForm data, int id){
            try{
                user_repository.UpdateUser(id, data);
                return Ok();
            }
            catch (ObjectNotFoundException e){
                return NotFound();
            }
        }


        // return users' submissoins in reverse order of their creation
        [HttpGet]
        [Route("{id}/submissions")]
        public IHttpActionResult UserSubmissions(int id, int start, int limit){
            return Ok(new CollectionResponse(){
                TotalCount = user_repository.GetUserSubmissionCount(id),
                Collection = SubmissionResponseData.MapTo(user_repository.GetUserSubmissions(id, start, limit))
            });
        }

        [HttpGet]
        [Route("{id}/contests")]
        public IHttpActionResult GetContestHistory(int id){
            return Ok(ContestListItem.MapTo(user_repository.GetUserContestHistory(id)));
        }


        [HttpGet]
        [Route("{id}/problems")]
        public IHttpActionResult GetUserProblems(int id, int start, int limit){
            var problems = user_repository.GetProblems(id, start, limit);
            
            
            return Ok(new CollectionResponse(){
                TotalCount = user_repository.GetProblemCount(id),
                Collection = ProblemListItem.MapTo(problems)
            });
        }
    }
}
