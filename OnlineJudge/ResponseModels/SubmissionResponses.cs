using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineJudge.Models;

namespace OnlineJudge.ResponseModels {
    class SubmissionResponseData{
        public string ProblemTitle { set; get; }
        public string Status { set; get; }
        public string UserName { set; get; }

        public SubmissionResponseData(Submission submission){
            ProblemTitle = submission.Problem.Title;
            Status = submission.Status.Name;
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
}