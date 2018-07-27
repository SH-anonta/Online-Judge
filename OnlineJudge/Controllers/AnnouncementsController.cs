using System.Collections.Generic;
using System.Web.Http;
using OnlineJudge.FormModels;
using OnlineJudge.Repository;
using OnlineJudge.ResponseModels;

namespace OnlineJudge.Controllers{
    [RoutePrefix("api/announcements")]
    public class AnnouncementsController : ApiController{


        [HttpGet]
        [Route("")]
        public IHttpActionResult AnnouncementList(){
            return Ok(DataRepository.GetAnnouncementList());
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult AnnouncementList(int from, int to){
            return Ok(DataRepository.GetAnnouncementList(from, to));
        }


        [HttpPost]
        [Route("create")]
        public IHttpActionResult Create([FromBody] AnnouncementForm data){
            DataRepository.createAnnouncement(data);
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult AnnoucementDetails(int id){
            return Ok(DataRepository.GetAnnouncementById(id));
        }

        [HttpPost]
        [Route("{id}/edit")]
        public IHttpActionResult EditAnnouncement(int id, [FromBody] AnnouncementForm data){
            DataRepository.UpdateAnnouncement(id, data);
            return Ok();
        }
        
    }
}
