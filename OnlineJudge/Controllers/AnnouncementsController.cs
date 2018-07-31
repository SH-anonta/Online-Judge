using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Web.Http;
using OnlineJudge.FormModels;
using OnlineJudge.Models;
using OnlineJudge.Repository;
using OnlineJudge.ResponseModels;

namespace OnlineJudge.Controllers{
    [RoutePrefix("api/announcements")]
    public class AnnouncementsController : ApiController{
        private AnnouncementRepository announcement_repository;

        // todo add object not found exception handlers to acitons 
        public AnnouncementsController(): base(){
            announcement_repository = new AnnouncementRepository();
        }

        public AnnouncementsController(AnnouncementRepository repo): base(){
            announcement_repository = repo;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult AnnouncementList(){
            var announcements = announcement_repository.GetAnnouncementList();
            return Ok(AnnouncementListItem.MapTo(announcements));
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult AnnouncementList(int from, int to){
            var announcements = announcement_repository.GetAnnouncementList(from, to);
            return Ok(AnnouncementListItem.MapTo(announcements));
        }

        [HttpPost]
        [Route("create")]
        public IHttpActionResult Create([FromBody] AnnouncementFormData data){
            announcement_repository.createAnnouncement(data);
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult AnnoucementDetails(int id){
            Announcement announcement = announcement_repository.GetAnnouncementById(id);
            return Ok(new AnnouncementsResponseData(announcement));
        }

        [HttpPost]
        [Route("{id}/edit")]
        public IHttpActionResult EditAnnouncement(int id, [FromBody] AnnouncementFormData data){
            announcement_repository.UpdateAnnouncement(id, data);
            return Ok();
        }

        [HttpPost]
        [Route("{id}/delete")]
        public IHttpActionResult DeleteAnnouncement(int id){
            try{
                announcement_repository.DeleteAnnouncement(id);
                return Ok();
            }
            catch (ObjectNotFoundException e){
                return NotFound();
            }
            
            
        }
        
    }
}
