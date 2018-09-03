using System.Data.Entity.Core;
using System.Web.Http;
using OnlineJudge.FormModels;
using OnlineJudge.Repository;
using OnlineJudge.ResponseModels;
using OnlineJudge.Services;

namespace OnlineJudge.Controllers{
    [RoutePrefix("api/users")]
    public class UserController : ApiController{
        private UserService user_service  = new UserService();
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

        [HttpOptions]
        [HttpPost]
        [Route("{id}/edit")]
        public IHttpActionResult UpdateUser([FromBody]UserProfileUpdateForm data, int id){
            if (!user_service.IsAuthorizedToEditUesrProfile(id)){
                return Unauthorized();
            }

            if (RequestUtility.IsPreFlightRequest(Request)){
                return Ok();
            }
            
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
            if (!user_service.IsAuthorizedToViewUserProblems(id)){
                return Unauthorized();
            }

            var problems = user_repository.GetProblems(id, start, limit);
            
            
            return Ok(new CollectionResponse(){
                TotalCount = user_repository.GetProblemCount(id),
                Collection = ProblemListItem.MapTo(problems)
            });
        }

        [HttpGet]
        [Route("types")]
        public IHttpActionResult GetUserTypes(){
            return Ok(UserTypeData.MapTo(user_repository.GetUserTypes()));
        }


        // returns 
        [HttpGet]
        [Route("my-state")]
        public IHttpActionResult GetUserState(){
            return Ok(user_service.GetUserState());
        }
    }
}
