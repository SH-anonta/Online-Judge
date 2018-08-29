using System;
using System.Collections.Generic;
using System.Linq;
using OnlineJudge.Models;

namespace OnlineJudge.ResponseModels {
    public class ContestListItem{
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

    public class ContestProblemListItemData{
        public int Id{ set; get; }      // ID of the actual problem entry
        public int Order{ set; get; }
        public string Title { set; get; }
        public int SolveCount { set; get; }

        public ContestProblemListItemData(ContestProblem problem){
            Id = problem.Problem.Id;

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

    public class ContestDetailsData{
        public int Id{ set; get; }
        public string Title { set; get; }
        public string Description { set; get; }
        
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
        
        public string Creator { set; get; }
        public int CreatorId { set; get; }

        public bool IsPublic { set; get; }
        
        public IEnumerable<ContestProblemListItemData> Problems { set; get; }

        public ContestDetailsData(Contest contest){
            this.Id = contest.Id;
            this.Title = contest.Title; 
            this.Description = contest.Description; 

            this.StartDate = contest.StartDate; 
            this.EndDate = contest.EndDate; 

            this.Creator = contest.Creator.UserName; 
            this.CreatorId = contest.Creator.Id;
            this.Problems = ContestProblemListItemData.MapTo(contest.Problems);

            this.IsPublic = contest.IsPublic;
        }

        public static List<ContestDetailsData> MapTo(IEnumerable<Contest> contests){
            var mapped = new List<ContestDetailsData>();

            foreach (var contest in contests){
                mapped.Add(new ContestDetailsData(contest));
            }

            return mapped;
        }
    }

    public class ContestantListItemData{
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

    public class ContestProblemDetailsData{
        public ProblemDetails problem_details { set; get; }
        public int Order { set; get; }

        public ContestProblemDetailsData(ContestProblem problem){
            this.problem_details = new ProblemDetails(problem.Problem);
            this.Order = problem.Order;
        }
    }

    public class ContestHistoryListItemData{
        // todo implement
    }

    public class ContestRankListItem{
        public int UserId{ set; get; }
        public string UserName{ set; get; }

        public int SolveCount { set; get; }
        public int Penalty { set; get; }
        
        // these two should contain information about submissions 
        public IEnumerable<TimeSpan?> ProblemAcceptTimes; // Submission time of first accepted submission for each problem
        public IEnumerable<int> ProblemRejectCounts;
        
        // expects to get 
        public ContestRankListItem(Contestant contestant, Contest contest){
            UserId = contestant.User.Id;
            UserName= contestant.User.UserName;
            SolveCount = contestant.SolveCount;
            Penalty = contestant.Penalty;


            var problem_submissions = contestant.Submissions.OrderBy(x=>x.Submission.SubmissionDate);

            var first_accept_time = new List<TimeSpan?>();
            List<int> reject_counts = new List<int>();
            

            //expects the submissions to be sorted in ascending order of submission date
            foreach (ContestProblem problem in contest.Problems){
                ContestSubmission first_accept_submission = problem_submissions.FirstOrDefault(x=>x.IsAccepted() && x.Problem == problem);
                first_accept_time.Add(first_accept_submission  == null ? (TimeSpan?) null : first_accept_submission.Submission.SubmissionDate - contest.StartDate);

                // either the time of first accepted submission or max value of date time
                var submission_time_limit = first_accept_submission == null
                    ? DateTime.MaxValue
                    : first_accept_submission.Submission.SubmissionDate;

                // count the number of rejected submissions before the first accepted one
                reject_counts.Add(problem_submissions.Count(x=>x.Problem == problem
                                                              && !x.IsAccepted()
                                                              && x.Submission.SubmissionDate < submission_time_limit));
            }

            ProblemAcceptTimes = first_accept_time;
            ProblemRejectCounts = reject_counts;
        }

        public static List<ContestRankListItem> MapTo(IEnumerable<Contestant> contestants, Contest contest){
            var mapped = new List<ContestRankListItem>();

            foreach (var contestant in contestants){
                mapped.Add(new ContestRankListItem(contestant, contest));
            }

            return mapped;
        }
    }

    public class ContestRankCollection{
        public int TotalCount { set; get; }
        public int RankStartsFrom{ set; get; }
        public IEnumerable<ContestRankListItem> Collection{ set; get; }
        public string ContestTitle { get; set; }
    }

    public class UnFinishedContestListCollection{
        public IEnumerable<ContestListItem> RunningContests;
        public IEnumerable<ContestListItem> UpcomingContests;

        public UnFinishedContestListCollection(IEnumerable<Contest> running_contests,IEnumerable<Contest> upcoming_contests){
            RunningContests = ContestListItem.MapTo(running_contests);
            UpcomingContests = ContestListItem.MapTo(upcoming_contests);
        }
    }

    public class ContestListCollection{
        public int TotalCount;
        public IEnumerable<ContestListItem> Collection;
    }

    public class ContestSubmissionListCollection{
        public int TotalCount;
        public IEnumerable<SubmissionResponseData> Collection;

    }


}