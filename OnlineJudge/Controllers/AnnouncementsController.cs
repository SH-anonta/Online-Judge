using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OnlineJudge.Repository;
using OnlineJudge.ResponseModels;

namespace OnlineJudge.Controllers{
    [RoutePrefix("api/announcements")]
    public class AnnouncementsController : ApiController{

        [HttpGet()]
        [Route("")]
        public string Index(int recent_from, int recent_to){
            // todo implement
//            DataRepository.GetRecentAnnouncements();

            return recent_from.ToString() + " " + recent_to.ToString();
        }

        [HttpGet()]
        [Route("all")]
        public List<AnnouncementsResponseData> All(){
            
            return DataRepository.GetAllAnnouncements();
        }
    }
}
