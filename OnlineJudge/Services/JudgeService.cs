using System;
using System.Linq;
using OnlineJudge.Controllers;
using OnlineJudge.Models;
using JudgeCodeRunner;
using OnlineJudge.FormModels;


namespace OnlineJudge.Services {
    public class JudgeService {
        public event EventHandler<ExecutionResultEventArgs> OnSubmissionStatusChange;

        private static readonly log4net.ILog logger =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // todo use a readonly binding of Problem
        public void judge(SubmissionFormData submissison, Problem problem){
            int problem_code = problem.Id;
            logger.Info(String.Format("Submission recieved from user {0}, for Problem {1}", 1, problem_code));
            
            var runner = new JudgeCodeRunner.CodeRunner((ProgrammingLanguageEnum) submissison.ProgrammingLanguageId,
                                                        submissison.SourceCode,
                                                        problem.TestCaseInput,
                                                        problem.TestCaseOutput,
                                                        problem.TimeLimit);

            
            runner.OnExecutionFinished += (sender, e) =>{
                logger.Info("Execution finished");
                OnSubmissionStatusChange(this, new ExecutionResultEventArgs(e.ExecutionResult));
            };

            runner.RunCode();

        }


        public void CreateSubmission(int problemId, SubmissionFormData data){
            throw new NotImplementedException();
        }
    }
}