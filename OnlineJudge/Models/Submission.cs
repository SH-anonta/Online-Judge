using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using JudgeCodeRunner;
using OnlineJudge.FormModels;

namespace OnlineJudge.Models {
    public class Submission{
        public int Id{set; get;}
        
        public string SourceCode{set; get;}
        public string StandardErrorStream{set; get;}

        [Column(TypeName = "datetime2")]
        public DateTime SubmissionDate { set; get; }

        public virtual Problem Problem{set; get;}
        public virtual User Submitter{set; get;}
        public virtual SubmissionStatus Status{set; get;}

        public double RunningTime {set; get;}
        public double PeakMemmoryUsage {set; get;}

        public virtual ProgrammingLanguage ProgrammingLanguage {set; get;}

        public Submission(){

        }

        public bool IsAccepted(){
            return this.Status.Id == Verdict.Accepted;
        }
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
        public ProgrammingLanguageEnum Id{set; get;}
        public string Name{set; get;}
    }
}