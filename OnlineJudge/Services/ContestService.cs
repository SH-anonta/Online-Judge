using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using JudgeCodeRunner;
using OnlineJudge.Models;

namespace OnlineJudge.Services {
    public class ContestService {
        private static readonly int NOT_ACCEPTED_PENALTY = 20;

        // this method assumes all ContestSubmission objects recieved via parameter belong to the same Contestant
        public int CalclatePenalty(IEnumerable<ContestSubmission> submissions){
            // at least one submission must be present in the submissoins list
            DateTime contest_start_time = submissions.First().Contest.StartDate;
            

            HashSet<ContestProblem> solved_problems= new HashSet<ContestProblem>();
            Dictionary<ContestProblem, int> penalty_per_problem= new Dictionary<ContestProblem, int>();

            // this algorithm requires the submissions to be ordered by their submisison data 
            submissions = submissions.OrderBy(x => x.Submission.SubmissionDate);

            // initialize each penalty for each problem to 0
            foreach (ContestProblem problem in submissions.First().Contest.Problems){
                penalty_per_problem.Add(problem, 0);
            }

            foreach (var submission in submissions){
                if (solved_problems.Contains(submission.Problem)){
                    // by skipping the rest of the loop
                    // we are excluding any submission made after an accepted submission (of the same problem)
                    continue;
                }

                var submission_status = submission.Submission.Status.Id;
                if (submission_status == Verdict.Accepted){
                    solved_problems.Add(submission.Problem);
                    penalty_per_problem[submission.Problem]+=  (submission.Submission.SubmissionDate - contest_start_time).Minutes;
                }
                else if (StatusIsJudgable(submission_status)){
                    // for any other status which is not accepted, running, queueed or compiling
                    penalty_per_problem[submission.Problem]+=  NOT_ACCEPTED_PENALTY;
                }
            }

            int penalty = 0;
            foreach (var problem in solved_problems){
                penalty += penalty_per_problem[problem];
            }
            return penalty;
        }

        // utility functions

        private bool StatusIsJudgable(Verdict verdict){
            return !(verdict == Verdict.Queueed || verdict == Verdict.Running || verdict == Verdict.Compiling);
        }
    }
}