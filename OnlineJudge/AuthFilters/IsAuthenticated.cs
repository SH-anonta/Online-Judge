
using System.Net;
using System.Net.Http.Headers;
using System.Web.Http.Controllers;
using System.Web.Http.Results;
using OnlineJudge.Services;

namespace Services {
    public class IsAuthenticated : System.Web.Http.AuthorizeAttribute
    {
        private UserService user_service;
        public IsAuthenticated(): base(){
            user_service = new UserService();
        }

        public override void OnAuthorization(HttpActionContext actionContext){
            if (!user_service.UserIsAuthenticated()){
//                actionContext.Response.StatusCode = HttpStatusCode.Unauthorized;
            }

            base.OnAuthorization(actionContext);
        }

    }
}