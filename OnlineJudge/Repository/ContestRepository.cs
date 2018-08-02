using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Diagnostics;
using System.Linq;
using System.Web;
using JudgeCodeRunner;
using OnlineJudge.FormModels;
using OnlineJudge.Models;
using OnlineJudge.ResponseModels;

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

        public void CreateSubmission(int contest_id, int problem_no, SubmissionFormData submission_data){
            Contest contest = context.Contests.Include(x=>x.Problems).FirstOrDefault(x=>x.Id == contest_id);
            ContestProblem contest_problem = contest.Problems.FirstOrDefault(x=>x.Order == problem_no);
            Problem problem = contest_problem.Problem;

            // important: problem_repository must be initialized using this.context
            ProblemRepository problem_repository = new ProblemRepository(this.context);
            //todo check for null values

            // todo fix
            User submitter = context.Users.First();

            Submission submission = problem_repository.CreateSubmission(submission_data);
//            Submission submission= new Submission(){
//                Submitter = submitter, 
//                Problem = problem,
//                ProgrammingLanguage = context.ProgrammingLanguages.Find(ProgrammingLanguageEnum.Cpp11),
//                Status = context.SubmissionStatus.Find(Verdict.Queueed)
//            };

            ContestSubmission contest_submission = new ContestSubmission(){
                Submitter = context.Contestants.First(x => x.User.Id == submitter.Id),
                Problem = contest_problem,
                Submission = submission,
            };

            context.ContestantSubmissions.Add(contest_submission);
            context.SaveChanges();

        }
    }
}
