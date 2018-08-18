using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Diagnostics;
using System.Linq;
using System.Web;
using JudgeCodeRunner;
using OnlineJudge.FormModels;
using OnlineJudge.Models;
using OnlineJudge.Services;

namespace OnlineJudge.Repository {
    public class SubmissionRepository{
        public event EventHandler<ExecutionResultEventArgs> OnSubmissionStatusChange;

        private readonly OjDBContext context;

        public SubmissionRepository(){
            context = new OjDBContext();
        }

        public SubmissionRepository(OjDBContext context){
            this.context = context;
        }

        public Submission InitializeSubmissionObject(Problem problem, SubmissionFormData submission_data){
            var language = context.ProgrammingLanguages.Find((ProgrammingLanguageEnum)submission_data.LanguageId);

            return new Submission(){
                Status = context.SubmissionStatus.Find(Verdict.Running),
                Problem = problem,
                SourceCode = submission_data.SourceCode,
                SubmissionDate = DateTime.Now,
                Submitter = context.Users.First(x => x.UserName == "admin"),
                ProgrammingLanguage = language
            };
        }

        public Submission CreateProblemSubmission(int problem_id, SubmissionFormData submission_data){
            Trace.WriteLine(submission_data.SourceCode);

            var problem = context.Problems.Find(problem_id);
            var judge = new JudgeService();

            if (problem == null){
                throw new ObjectNotFoundException("Problem with specified id does not exist");
            }

//            Trace.WriteLine(submission_data.LanguageId);
            var submission = InitializeSubmissionObject(problem, submission_data);

            context.Submissions.Add(submission);
            context.SaveChanges();

            judge.OnSubmissionStatusChange += (sender, e) =>{
                var result = e.ExecutionResult;
                submission.Status = context.SubmissionStatus.Find(result.Verdict);
                submission.RunningTime = result.RunningTime;
                submission.PeakMemmoryUsage = result.MemmoryUsage;
                submission.StandardErrorStream= result.ErrorMsg;

                context.SaveChanges();
            };

            judge.judge(submission_data, problem);

            return submission;
        }

    }
}