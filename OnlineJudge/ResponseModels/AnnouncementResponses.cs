using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineJudge.Models;

namespace OnlineJudge.ResponseModels {

    public class AnnouncementsResponseData{
        public int Id{ set; get; }
        public string Title{ set; get; }
        public string Description{ set; get; }
        public string Creator{ set; get; }
        public DateTime CreateDate{ set; get; }

        public AnnouncementsResponseData(Announcement data){
            this.Id = data.Id;
            this.Title = data.Title;
            this.Description = data.Description;
            this.CreateDate = data.CreateDate;
            this.Creator = data.Creator.UserName;
        }

        public static List<AnnouncementsResponseData> MapTo(IQueryable<Announcement> announcements){
            var mapped = new List<AnnouncementsResponseData>();

            foreach (var announcement in announcements){
                mapped.Add(new AnnouncementsResponseData(announcement));
            }

            return mapped;
        }
    }

    public class AnnouncementListItem{
        public int Id{ set; get; }
        public string Title{ set; get; }
        public string Creator{ set; get; }
        public DateTime CreateDate{ set; get; }

        public AnnouncementListItem(Announcement data){
            this.Id = data.Id;
            this.Title = data.Title;
            this.CreateDate = data.CreateDate;
            this.Creator = data.Creator.UserName;
        }

        public static List<AnnouncementListItem> MapTo(IQueryable<Announcement> announcements){
            var mapped = new List<AnnouncementListItem>();

            foreach (var announcement in announcements){
                mapped.Add(new AnnouncementListItem(announcement));
            }

            return mapped;
        }
    }
}