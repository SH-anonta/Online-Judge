using System;
using System.Collections.Generic;
using System.Linq;
using OnlineJudge.Models;

namespace OnlineJudge.Controllers {
    class SubmissionResponseData{
        public string ProblemTitle { set; get; }
        public string Status { set; get; }
        public string UserName { set; get; }

        public SubmissionResponseData(Submission submission){
            ProblemTitle = submission.Problem.Title;
            Status = submission.Status;
            UserName  = "Anna";
        }

        public static List<SubmissionResponseData> MapTo(IQueryable<Submission> subs){
            var mapped = new List<SubmissionResponseData>();

            foreach (var sub in subs){
                mapped.Add(new SubmissionResponseData(sub));
            }

            return mapped;
        }
    }

    class AnnouncementsResponseData{
        public string Title{ set; get; }
        public string Description{ set; get; }
        public string Creator{ set; get; }
        public DateTime CreateDate{ set; get; }

        public AnnouncementsResponseData(Announcement data){
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
}