using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineJudge.Models {
    public class Submission{
        public int Id{set; get;}
        
        public string SourceCode{set; get;}
        public string StandardErrorStream{set; get;}

        public DateTime SubmissionDate { set; get; }

        public virtual Problem Problem{set; get;}
        public virtual User Submitter{set; get;}

        public double RunningTime {set; get;}
        public double PeakMemmoryUsage {set; get;}
        
        // todo use verdict model instead of string
        public String Status{set; get;}

        // todo add programming language FK
        public string ProgrammingLanguage {set; get;}

    }

    public class SubmissionStatus{
        public int Id{set; get;}
        public string Name{set; get;}
    }

    public class ProgrammingLanguage{
        public int Id{set; get;}
        public string Name{set; get;}
    }
}