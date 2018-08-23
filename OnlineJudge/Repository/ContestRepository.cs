using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Web;
using JudgeCodeRunner;
using OnlineJudge.FormModels;
using OnlineJudge.Models;
using OnlineJudge.ResponseModels;
using OnlineJudge.Services;

namespace OnlineJudge.Repository {
    public class ContestRepository{
        private OjDBContext context;

        public ContestRepository(){
            this.context= new OjDBContext();
        }

        public ContestRepository(OjDBContext context){
            this.context= context;
        }

        public IEnumerable<Contest> GetAllContestsList(){
            return from contest in context.Contests
                orderby contest.StartDate
                select contest;
        }

        public IEnumerable<Contest> GetRecentContestsList(int from, int to){
            var contests = GetAllContestsList();
            return contests.Skip(from - 1).Take(to-from+1);
        }

        public Contest GetContestById(int contest_id){
            Contest contest = context.Contests.Include(x => x.Problems).FirstOrDefault(x => x.Id== contest_id);

            if (contest == null){
                throw new ObjectNotFoundException("Contest entry with specified id not found");
            }

            return contest;
        }

        public Contestant RegisterUserForContest(int contest_id, int user_id){
            Contest contest = context.Contests.Include(x=> x.Contestants).FirstOrDefault(x => x.Id == contest_id);
            
            var user = context.Users.Find(user_id);

            if (contest == null){
                throw new ObjectNotFoundException("Contest with specified id not found");
            }

            if (user == null){
                throw new ObjectNotFoundException("User with specified id not found");
            }

            // if user is already registered for this contest
            if (contest.Contestants.Count(x => x.User == user) > 0){
                throw new InvalidOperationException("User is already registered for contest");
            }

            if (contest.StartDate < DateTime.Now){
                throw new InvalidOperationException("Contest registration time has ended");
            }

            Trace.WriteLine(contest.Contestants.Count);
            var contestant = new Contestant(user);
            
            contest.Contestants.Add(contestant);
            context.SaveChanges();
            
            return contestant;
        }

        public IEnumerable<Contestant> GetContestantsOfContest(int contest_id){
            var contest = context.Contests.Include(x=>x.Contestants).FirstOrDefault(x => x.Id == contest_id);

            if (contest == null){
                throw new ObjectNotFoundException();
            }

            return contest.Contestants;
        }

        public IEnumerable<ContestProblem> GetContestProblems(int contest_id){
            Contest contest = context.Contests.Include(x=> x.Problems).FirstOrDefault(x => x.Id == contest_id);

            if (contest == null){
                throw new ObjectNotFoundException("Contest with specified n");
            }

            return contest.Problems;
        }

        public ContestProblem GetContestProblem(int contest_id, int problem_order){
            Contest contest = context.Contests.Include(x=> x.Problems).FirstOrDefault(x => x.Id == contest_id);

            if (contest == null){
                throw new ObjectNotFoundException("Contest with specified n");
            }

            return contest.Problems.First(x => x.Order == problem_order);
        }

        

        public IEnumerable<Submission> GetAllSubmissions(int contest_id){
            Contest contest = context.Contests.Include(x => x.Submissions).FirstOrDefault(x=>x.Id == contest_id);

            if (contest == null){
                throw new ObjectDisposedException("Contest with specified id not found");
            }

            return contest.Submissions.Select(x=>x.Submission);
        }

        public IEnumerable<Submission> GetContestantSubmissions(int contest_id, int user_id){
            // todo add null value checks
            Contestant contestant = context.Contestants.Include(x => x.Submissions)
                                                       .FirstOrDefault(x => x.Contest.Id == contest_id 
                                                            && x.User.Id == user_id);
            if(contestant == null){
                throw new ObjectDisposedException("Contest with specified id not found");
            }

            return contestant.Submissions.Select(x=>x.Submission);
        }

        public IEnumerable<Submission> GetContestantProblemSubmissions(int contest_id, int problem_no){
            var problem = context.ContestProblems.Include(x => x.Submissions)
                .FirstOrDefault(x => x.Contest.Id == contest_id && x.Order == problem_no);

            return problem.Submissions.Select(x => x.Submission);
        }

        public void CreateContest(int user_id, ContestCreationFormData data){

            List<ContestProblem> contest_problems = new List<ContestProblem>();
            var creator = context.Users.Find(user_id);

            foreach(int problem_id in data.Problems){
                var problem = context.Problems.Find(problem_id);
                

                if (problem == null){
                    throw new InvalidOperationException("One or more specified problem IDs were not found");
                }

                contest_problems.Add(new ContestProblem(){
                    Problem = problem,
                });
            }

            Contest contest = new Contest(){
                Title = data.Title,
                Description = data.Description,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                Problems = contest_problems,
                Creator = creator
            };

            context.Contests.Add(contest);
            context.SaveChanges();
        }


        public int GetContestCount(int problem_id){
            return context.Contests.Count();
        }

        public int GetContestSubmissionCount(int contest_id){
            return context.ContestantSubmissions.Count(x=>x.Id == contest_id);
        }

        public int GetContestSubmissionOfProblemCount(int contest_id){
            return context.ContestantSubmissions.Count(x=>x.Problem.Contest.Id == contest_id);
        }

        public int GetContestSubmissionOfUserCount(int contest_id, int user_id){
            return context.ContestantSubmissions.Count(x=>x.Problem.Contest.Id == contest_id
                                                          && x.Submitter.User.Id == user_id);
        }

        // rank list sorted in order of descending solve count and then ascending penalty
        public ContestRankCollection GetRankList(int contest_id, int start= 1, int limit = 100){
            Contest contest = context.Contests.Include(x=>x.Problems).FirstOrDefault(x=>x.Id == contest_id);
            

            if (contest == null){
                throw new ObjectNotFoundException("Contest with specified ID not found");
            }

            var contestants = context.Contestants.Include(x=>x.Submissions).Where(x => x.Contest.Id == contest_id && x.Submissions.Count > 0);
            
            // number of contestants who have at least one submission
            int contestants_count = contestants.Count();

            contestants = contestants.OrderByDescending(x=> x.SolveCount).ThenBy(x => x.Penalty);
            contestants = contestants.Skip(start-1).Take(limit-start+1);

            ContestRankCollection rank_list = new ContestRankCollection(){
                ContestTitle = contest.Title,
                RankStartsFrom = start,
                Collection = ContestRankListItem.MapTo(contestants, contest),
                TotalCount = contestants_count
            };

            return rank_list;
        }

        public void DeleteContest(int contest_id){
            Contest contest = GetContestById(contest_id);
            context.Contests.Remove(contest);
            context.SaveChanges();
        }

        public UnFinishedContestListCollection GetUnfinishedContests(){
            var all_contests = context.Contests.OrderByDescending(x => x.StartDate);
            var running = all_contests.Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now);
            var upcoming = all_contests.Where(x => x.StartDate > DateTime.Now);
            
            return new UnFinishedContestListCollection(running, upcoming);
        }

        public IEnumerable<Contest> GetPastContests(int start, int limit){
            // select contests that have ended
            var contests = context.Contests.OrderByDescending(x=>x.StartDate).Where(x=>x.EndDate < DateTime.Now);
            return contests.Skip(start - 1).Take(limit-start+1);
        }
    }

    class ContestSubmissionRepository{
        private OjDBContext context;
        private ContestService contest_service;

        public ContestSubmissionRepository(){
            this.context= new OjDBContext();
            this.contest_service= new ContestService();
        }


        public void CreateSubmission(int contest_id, int problem_no, SubmissionFormData submission_data){
            // important: submission_repository must be initialized using this.context
            SubmissionRepository submission_repository= new SubmissionRepository(context);
            
            Contest contest = context.Contests.Include(x=>x.Problems).FirstOrDefault(x=>x.Id == contest_id);

            if (contest.EndDate < DateTime.Now){
                throw new InvalidOperationException("Can not create submission to contest that has ended");
            }

            if (contest.StartDate > DateTime.Now){
                throw new InvalidOperationException("Can not create submission to contest that has not started");
            }

            ContestProblem contest_problem = contest.Problems.FirstOrDefault(x=>x.Order == problem_no);
            Problem problem = contest_problem.Problem;

            submission_repository.OnSubmissionStatusChange += SubmissionStatusUpdateHandler;

            //todo check for null values

            // todo fix
            User submitter = context.Users.First();

            Submission submission = submission_repository.CreateProblemSubmission(problem.Id, submission_data);

            ContestSubmission contest_submission = new ContestSubmission(){
                Submitter = context.Contestants.First(x => x.User.Id == submitter.Id),
                Problem = contest_problem,
                Submission = submission,
            };

            contest.Submissions.Add(contest_submission);
            context.SaveChanges();
        }


        private void SubmissionStatusUpdateHandler(Object sender, ExecutionResultEventArgs args){
            // the submission entry is updated by SubmissionRepository,
            // this handler only updates the contestant

            // replace with real user
            var submitter = context.Contestants.Include(x => x.Submissions).First(x=> x.Id == 1);

            submitter.Penalty = contest_service.CalclatePenalty(submitter.Submissions);
            
            submitter.SolveCount = context.ContestProblems.
                                    Count(x=>x.Submissions.
                                    Count(y=>y.Submission.Status.Id == Verdict.Accepted) > 0);
            context.SaveChanges();
        }
    }
}
