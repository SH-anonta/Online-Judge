using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using JudgeCodeRunner;

namespace OnlineJudge.Models {
    public class Submission{
        public int Id{set; get;}
        
        public string SourceCode{set; get;}
        public string StandardErrorStream{set; get;}

        public DateTime SubmissionDate { set; get; }

        public virtual Problem Problem{set; get;}
        public virtual User Submitter{set; get;}
        public virtual SubmissionStatus Status{set; get;}

        public double RunningTime {set; get;}
        public double PeakMemmoryUsage {set; get;}

        // todo add programming language FK
        public string ProgrammingLanguage {set; get;}

    }

    public class SubmissionStatus{

        // enum is being used as Id
        public Verdict Id{set; get;}
        public string Name{set; get;}


        // this seed data is meant to be part of the production
        public static List<SubmissionStatus> getSeedData(){
            var list = new List<SubmissionStatus>();

            var enum_values = Enum.GetValues(typeof(Verdict)).Cast<Verdict>();

            foreach (Verdict value in enum_values){
                list.Add(new SubmissionStatus(){
                    Id= value,
                    Name = value.ToString(),
                });
            }
            
            return list;
        }

    }

    public class ProgrammingLanguage{
        public int Id{set; get;}
        public string Name{set; get;}
    }
}