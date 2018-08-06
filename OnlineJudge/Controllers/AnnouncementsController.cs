using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
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
            return Ok(new CollectionResponse(){
                TotalCount = announcement_repository.GetAnnouncementCount(),
                Collection = AnnouncementListItem.MapTo(announcements)
            });
        }


        [HttpPost]
        [HttpOptions]
        [Route("create")]
        public IHttpActionResult Create([FromBody] AnnouncementFormData data){
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
