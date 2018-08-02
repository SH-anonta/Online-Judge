using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineJudge.FormModels {
    public class SubmissionFormData{
        public string ProblemCode { set; get; }
        public string SolutionCode { set; get; }
        public int LanguageId { set; get; }
    }
}