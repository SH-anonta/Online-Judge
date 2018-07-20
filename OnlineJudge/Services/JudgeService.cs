using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using JudgeCodeRunner;
using OnlineJudge.Controllers;
using OnlineJudge.Models;


namespace OnlineJudge.Services {
    public class JudgeService {
        private static readonly log4net.ILog logger =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void judge(SubmissionRequestData submisison){
            logger.Info("Submission recieved");

            var ctx = new OjDBContext();
            var problem = ctx.Problems.Find(1);

            var runner = new CodeRunner(submisison.SolutionCode,
                                        problem.TestCaseInput,
                                        problem.TestCaseOutput,
                                        problem.TimeLimit);

            var submission = new Submission()
            {
                Status = "Running",
                Problem = problem,
                SolutionCode =  submisison.SolutionCode
            };

            ctx.Submissions.Add(submission);
            ctx.SaveChanges();
                

            runner.OnExecutionFinished += (sender, e) =>{
                logger.Info("Execution finished");

                var result = e.ExecutionResult;
                submission.Status = result.Verdict.ToString();

                ctx.SaveChanges();
            };

            runner.RunCode();

        }
    }
}