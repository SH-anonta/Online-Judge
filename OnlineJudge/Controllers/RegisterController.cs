using System.Web.Http;
using OnlineJudge.FormModels;
using OnlineJudge.Repository;

namespace OnlineJudge.Controllers{
    
    
    [RoutePrefix("api/register")]
    public class RegisterController : ApiController{
        private UserRepository user_repository;

        public RegisterController(){
            this.user_repository = new UserRepository();
        }

        public RegisterController(UserRepository user_repository){
            this.user_repository = user_repository;
        }

        [HttpPost]
        [HttpOptions]
        public IHttpActionResult Register([FromBody] UserRegistrationFormData data){
            if (RequestUtility.IsPreFlightRequest(Request)){
                return Ok();
            }

            var result = data.Validate();

            if (!result.IsValid){
                return new BadHttpRequest(result.ErrorMessages);
            }
            user_repository.CreateNewUser(data);
            return Ok();
        }
    }
}
