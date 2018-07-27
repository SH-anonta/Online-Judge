using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineJudge.Models;

namespace OnlineJudge.ResponseModels {
    public class ProblemListItem{
        public int Id{ set; get; }

        public string Title { set; get; }
        public string Creator { set; get; }
        public bool IsPublic { set; get; }
        public DateTime CreateDate { set; get; }


        public ProblemListItem(Problem problem){
            this.Id = problem.Id;
            this.Title = problem.Title; 
            this.Creator = problem.Creator.UserName; 
            this.CreateDate = problem.CreateDate; 
            this.IsPublic = problem.IsPublic; 
        }

        public static List<ProblemListItem> MapTo(IQueryable<Problem> problems){
            var mapped = new List<ProblemListItem>();

            foreach (var problem in problems){
                mapped.Add(new ProblemListItem(problem));
            }

            return mapped;
        }
    }

    public class ProblemDetails{
        public int Id{ set; get; }
        public string Title { set; get; }
        public string Description { set; get; }
        public string Constraints { set; get; }
        
        public string InputSpecification { set; get; }
        public string OutputSpecification { set; get; }
        
        public string SampleInput { set; get; }
        public string SampleOutput { set; get; }
        
        public string Notes { set; get; }
        
        public double TimeLimit { set; get; }
        public double MemoryLimit { set; get; }

        public string Creator { set; get; }
        public DateTime CreateDate { set; get; }

        public bool IsPublic { set; get; }


        public ProblemDetails(Problem problem){
            this.Id = problem.Id;
            this.Title = problem.Title; 
            this.Description = problem.Description; 
            this.Constraints = problem.Constraints; 
            this.InputSpecification = problem.InputSpecification; 
            this.OutputSpecification = problem.OutputSpecification; 
            this.SampleInput = problem.SampleInput; 
            this.SampleOutput = problem.SampleOutput; 
            this.Notes = problem.Notes; 
            this.TimeLimit = problem.TimeLimit; 
            this.MemoryLimit = problem.MemoryLimit; 
            
            this.Creator = problem.Creator.UserName; 
            this.CreateDate = problem.CreateDate;
            this.IsPublic = problem.IsPublic; 
        }

        public static List<ProblemDetails> MapTo(IQueryable<Problem> problems){
            var mapped = new List<ProblemDetails>();

            foreach (var problem in problems){
                mapped.Add(new ProblemDetails(problem));
            }

            return mapped;
        }
    }
}