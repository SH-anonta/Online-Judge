using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineJudge.Models {
    public class Submission{
        public int Id{set; get;}
        public virtual Problem Problem{set; get;}
        
        // todo use verdict model instead of string
        public String Status{set; get;}

        // todo add programming language FK

        public String SolutionCode{set; get;}
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