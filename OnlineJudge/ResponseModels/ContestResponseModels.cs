using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebSockets;
using OnlineJudge.Models;

namespace OnlineJudge.ResponseModels {
    class ContestListItem{
        public int Id{ set; get; }
        public string Title { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
        public int CreatorId { set; get; }
        public string Creator { set; get; }

        public ContestListItem(Contest contest){
            this.Id = contest.Id;
            this.Title = contest.Title; 
            this.StartDate = contest.StartDate; 
            this.EndDate = contest.EndDate; 
            this.Creator = contest.Creator.UserName; 
            this.CreatorId = contest.Creator.Id; 
        }


        public static List<ContestListItem> MapTo(IEnumerable<Contest> contests){
            var mapped = new List<ContestListItem>();

            foreach (var contest in contests){
                mapped.Add(new ContestListItem(contest));
            }

            return mapped;
        }
    }

    class ContestProblemListItemData{
        public int Order{ set; get; }
        public string Title { set; get; }
        public int SolveCount { set; get; }

        public ContestProblemListItemData(ContestProblem problem){
            Order = problem.Order;
            Title = problem.Problem.Title;
            SolveCount = 0; // todo replace with actual value
        }

        public static List<ContestProblemListItemData> MapTo(IEnumerable<ContestProblem> problems){
            var mapped = new List<ContestProblemListItemData>();

            foreach (var problem in problems){
                mapped.Add(new ContestProblemListItemData(problem));
            }

            return mapped;
        }
    }

    class ContestDetailsData{
        public int Id{ set; get; }
        public string Title { set; get; }
        public string Description { set; get; }
        
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
        
        public string Creator { set; get; }
        public int CreatorId { set; get; }
        
        public IEnumerable<ContestProblemListItemData> Problems { set; get; }

        public ContestDetailsData(Contest contest){
            this.Id = contest.Id;
            this.Title = contest.Title; 
            this.StartDate = contest.StartDate; 
            this.EndDate = contest.EndDate; 
            this.Creator = contest.Creator.UserName; 
            this.CreatorId = contest.Creator.Id;
            this.Problems = ContestProblemListItemData.MapTo(contest.Problems);
        }

        public static List<ContestDetailsData> MapTo(IEnumerable<Contest> contests){
            var mapped = new List<ContestDetailsData>();

            foreach (var contest in contests){
                mapped.Add(new ContestDetailsData(contest));
            }

            return mapped;
        }
    }

    class ContestantListItemData{
        public int UserId { set; get; }
        public string UserName { set; get; }
        public int Penalty { set; get; }
        public int SolveCount { set; get; }


        public ContestantListItemData(Contestant contestant){
            this.UserId = contestant.User.Id;
            this.UserName = contestant.User.UserName;
            this.Penalty= contestant.Penalty;
            this.SolveCount= contestant.SolveCount;
        }

        public static List<ContestantListItemData> MapTo(IEnumerable<Contestant> contestants){
            var mapped = new List<ContestantListItemData>();

            foreach (var contestant in contestants){
                mapped.Add(new ContestantListItemData(contestant));
            }

            return mapped;
        }
    }

    class ContestProblemDetailsData{
        public ProblemDetails problem_details { set; get; }
        public int Order { set; get; }

        public ContestProblemDetailsData(ContestProblem problem){
            this.problem_details = new ProblemDetails(problem.Problem);
            this.Order = problem.Order;
        }
    }

    class ContestHistoryListItemData{
        // todo implement
    }
}