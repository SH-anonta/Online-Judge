using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineJudge.Controllers
{
    [RoutePrefix("api/temp")]
    public class TempController : ApiController{
        [Route("")]
        [HttpGet]
        public string Temp(){
            return "temp";
        }

    }
}
