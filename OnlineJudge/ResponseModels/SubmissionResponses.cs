using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineJudge.Models;

namespace OnlineJudge.ResponseModels {
    class SubmissionResponseData{
        public string ProblemTitle { set; get; }
        public int ProblemId { set; get; }

        public string ProgrammingLanguage { set; get; }
        public string Status { set; get; }
        public string UserName { set; get; }
        public int UserId { set; get; }

        public string SourceCode{set; get;}
        public string StandardErrorStream{set; get;}

        public DateTime SubmissionDate { set; get; }

        public double RunningTime {set; get;}
        public double PeakMemoryUsage {set; get;}

        public SubmissionResponseData(Submission submission){
            this.ProblemTitle = submission.Problem.Title;
            this.ProblemId = submission.Problem.Id;

            this.Status = submission.Status.Name;
            this.ProgrammingLanguage  = submission.ProgrammingLanguage.Name;

            this.UserName = submission.Submitter.UserName;
            this.UserId = submission.Submitter.Id;

            this.SourceCode = submission.SourceCode;
            this.StandardErrorStream = submission.StandardErrorStream;
            this.SubmissionDate = submission.SubmissionDate;
            
            this.RunningTime = submission.RunningTime;
            this.PeakMemoryUsage = submission.PeakMemmoryUsage;
        }

        public static List<SubmissionResponseData> MapTo(IEnumerable<Submission> subs){
            var mapped = new List<SubmissionResponseData>();

            foreach (var sub in subs){
                mapped.Add(new SubmissionResponseData(sub));
            }

            return mapped;
        }
    }

}