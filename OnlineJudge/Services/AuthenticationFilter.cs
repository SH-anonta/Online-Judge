using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace Services {
    public class AuthenticationFilter : AuthorizationFilterAttribute{

        public override void OnAuthorization(HttpActionContext actionContext){
            
        }

    }
}