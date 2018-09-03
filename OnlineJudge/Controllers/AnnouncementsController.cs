using System.Data.Entity.Core;
using System.Web.Http;
using OnlineJudge.FormModels;
using OnlineJudge.Models;
using OnlineJudge.Repository;
using OnlineJudge.ResponseModels;
using OnlineJudge.Services;

namespace OnlineJudge.Controllers{
    [RoutePrefix("api/announcements")]
    public class AnnouncementsController : ApiController{
        private UserService user_service;
        private AnnouncementRepository announcement_repository;

        // todo add object not found exception handlers to acitons 
        public AnnouncementsController(): base(){
            announcement_repository = new AnnouncementRepository();
            user_service= new UserService();
        }

        public AnnouncementsController(AnnouncementRepository repo): base(){
            announcement_repository = repo;
            user_service= new UserService();
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
            return Ok(new CollectionResponse(){
                TotalCount = announcement_repository.GetAnnouncementCount(),
                Collection = AnnouncementListItem.MapTo(announcements)
            });
        }


        [HttpPost]
        [HttpOptions]
        [Route("create")]
        public IHttpActionResult Create([FromBody] AnnouncementFormData data){
            if (!user_service.IsAuthorizedToCreateAnnouncements()){
                return Unauthorized();
            }

            // for preflight requests
            if (RequestUtility.IsPreFlightRequest(Request)){
                return Ok();
            }

            var result = data.Validate();
            if (!result.IsValid){
                return new BadHttpRequest(result.ErrorMessages);
            }

            var announcement = announcement_repository.createAnnouncement(data);
            return Ok(new AnnouncementsResponseData(announcement));
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult AnnoucementDetails(int id){
            try{
                Announcement announcement = announcement_repository.GetAnnouncementById(id);
                return Ok(new AnnouncementsResponseData(announcement));
            }
            catch (ObjectNotFoundException e){
                return NotFound();
            }
        }


        [HttpPost]
        [HttpOptions]
        [Route("{id}/edit")]
        public IHttpActionResult EditAnnouncement(int id, [FromBody] AnnouncementFormData data){
            if (!user_service.IsAuthorizedToEditAnnouncements(id)){
                return Unauthorized();
            }

            // for preflight requests
            if (RequestUtility.IsPreFlightRequest(Request)){
                return Ok();
            }

            var result = data.Validate();
            if (!result.IsValid){
                return new BadHttpRequest(result.ErrorMessages);
            }
            
            announcement_repository.UpdateAnnouncement(id, data);
            return Ok();
        }

        [HttpPost]
        [Route("{id}/delete")]
        public IHttpActionResult DeleteAnnouncement(int id){
            if (!user_service.IsAuthorizedToDeleteAnnouncements()){
                return Unauthorized();
            }

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
