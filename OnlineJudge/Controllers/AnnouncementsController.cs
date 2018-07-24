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
        public List<AnnouncementListItem> AnnouncementList(){
            return DataRepository.GetAnnouncementList();
        }

        [HttpPost]
        [Route("create")]
        public ResponseMessage Create([FromBody] AnnouncementForm data){
            DataRepository.createAnnouncement(data);
            return new ResponseMessage(){Success = true};
        }

        [HttpGet]
        [Route("{id}")]
        public AnnouncementsResponseData AnnoucementDetails(int id){
            return DataRepository.GetAnnouncementById(id);
        }
        
    }
}
