using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Web;
using System.Web.Http;
using OnlineJudge.FormModels;
using OnlineJudge.Models;
using OnlineJudge.ResponseModels;

namespace OnlineJudge.Repository {
    public partial class DataRepository {

        // sort the rows in descending order by create date
        // return skip the first 'from'-1 rows and then return the next 'to' rows
        public static List<AnnouncementListItem> GetAnnouncementList(int from, int to = 20){
            var ctx = getContext();
            var rows = from s in ctx.Announcements
                orderby s.CreateDate
                descending
                select s;

            return AnnouncementListItem.MapTo(rows.Skip(from-1).Take(to));
        }

        public static List<AnnouncementListItem> GetAnnouncementList(){
            var ctx = getContext();
            var rows = from s in ctx.Announcements
                orderby s.CreateDate
                descending 
                select s;

            return AnnouncementListItem.MapTo(rows);
        }      

        public static AnnouncementsResponseData GetAnnouncementById(int id){
            var announcement = getContext().Announcements.Find(id);
            return new AnnouncementsResponseData(announcement);
        }

        public static void createAnnouncement(AnnouncementForm data){
            var ctx = getContext();
            Announcement announcement = new Announcement(){
                Title = data.Title,
                Description = data.Description,
                CreateDate = DateTime.Now,
                Creator = ctx.Users.First(x => x.UserName == "admin")
            };

            ctx.Announcements.Add(announcement);
            ctx.SaveChanges();
        }

        public static void UpdateAnnouncement(int id, AnnouncementForm data)
        {
            var ctx = getContext();
            var announcement = ctx.Announcements.Find(id);
            announcement.Title = data.Title;
            announcement.Description = data.Description ;
            ctx.SaveChanges();
        }

        public static void DeleteAnnouncement(int id){
            var ctx = getContext();
            var announcement = ctx.Announcements.Find(id);

            if(announcement == null){ throw new ObjectNotFoundException();}

            ctx.Announcements.Remove(announcement);
            ctx.SaveChanges();
        }
    }
}