using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Web;
using System.Web.Http;
using OnlineJudge.FormModels;
using OnlineJudge.Models;
using OnlineJudge.ResponseModels;

namespace OnlineJudge.Repository {
    public partial class AnnouncementRepository {
        public OjDBContext context = new OjDBContext();

        public AnnouncementRepository(OjDBContext context){
            this.context = context;
        }

        public AnnouncementRepository(){
            this.context = new OjDBContext();
        }

        // sort the rows in descending order by create date
        // return skip the first 'from'-1 rows and then return the next 'to' rows
        public List<AnnouncementListItem> GetAnnouncementList(int from, int to = 20){
            var rows = from s in context.Announcements
                orderby s.CreateDate
                descending
                select s;

            return AnnouncementListItem.MapTo(rows.Skip(from-1).Take(to-from+1));
        }

        public List<AnnouncementListItem> GetAnnouncementList(){
            var rows = from s in context.Announcements
                orderby s.CreateDate
                descending 
                select s;

            return AnnouncementListItem.MapTo(rows);
        }      

        public AnnouncementsResponseData GetAnnouncementById(int id){
            var announcement = context.Announcements.Find(id);
            return new AnnouncementsResponseData(announcement);
        }

        public void createAnnouncement(AnnouncementFormData data){
            Announcement announcement = new Announcement(){
                Title = data.Title,
                Description = data.Description,
                CreateDate = DateTime.Now,
                Creator = context.Users.First(x => x.UserName == "admin")
            };

            context.Announcements.Add(announcement);
            context.SaveChanges();
        }

        public void UpdateAnnouncement(int id, AnnouncementFormData data){
            var announcement = context.Announcements.Find(id);
            announcement.Title = data.Title;
            announcement.Description = data.Description ;
            context.SaveChanges();
        }

        public void DeleteAnnouncement(int id){
            var announcement = context.Announcements.Find(id);

            if(announcement == null){ throw new ObjectNotFoundException();}

            context.Announcements.Remove(announcement);
            context.SaveChanges();
        }
    }
}