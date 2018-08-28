﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using JudgeCodeRunner;

namespace OnlineJudge.Models {
    public class Contest {
        public int Id{ set; get; }
        public string Title { set; get; }
        public string Description { set; get; }
        
        public string Password { set; get; }
        public bool IsPublic { set; get; }


        [Column(TypeName = "DateTime2")]
        public DateTime StartDate { set; get; }
        [Column(TypeName = "DateTime2")]
        public DateTime EndDate { set; get; }
        
        public virtual User Creator { set; get; }
        
        public ICollection<ContestProblem> Problems { set; get; }
        public ICollection<Contestant> Contestants { set; get; }
        public ICollection<ContestSubmission> Submissions { set; get; }

        public Contest(){
            this.Problems = new HashSet<ContestProblem>();
            this.Contestants = new HashSet<Contestant>();
            this.Submissions = new HashSet<ContestSubmission>();
        }
    }

    public class ContestProblem {
        public int Id{ set; get; }
        // todo make auto increment
        public int Order { set; get; }
        
        public virtual Contest Contest { set; get; }
        public virtual Problem Problem { set; get; }
        
        public ICollection<ContestSubmission> Submissions { set; get; }
    }


    public class Contestant{
        public int Id{ set; get; }
        public int SolveCount { set; get; }
        public int Penalty { set; get; }


        public virtual User User { set; get; }
        public virtual Contest Contest { set; get; }

        public ICollection<ContestSubmission> Submissions { set; get; }

        public Contestant(){}

        public Contestant(User user){
            this.User = user;
            this.SolveCount = 0;
            this.Penalty = 0;

            this.Submissions = new HashSet<ContestSubmission>();
        }
    }

    public class ContestSubmission{
        public int Id{ set; get; }
        public int Score { set; get; }

        public virtual Submission Submission { set; get; }
        public virtual Contestant Submitter { set; get; }
        public virtual Contest Contest { set; get; }
        public virtual ContestProblem Problem { set; get; }

        public bool IsAccepted(){
            return this.Submission.Status.Id == Verdict.Accepted;
        }
    }
}