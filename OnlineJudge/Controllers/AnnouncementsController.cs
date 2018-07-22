using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OnlineJudge.Repository;

namespace OnlineJudge.Controllers{
    [RoutePrefix("api/announcements")]
    public class AnnouncementsController : ApiController{

        [HttpGet]
        [Route("")]
        public void Index(){
            // todo implement
//            DataRepository.
        }
    }
}
