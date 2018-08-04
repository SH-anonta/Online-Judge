using System;
using System.Data.Entity.Core;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Web;
using System.Web.WebPages;
using JudgeCodeRunner;
using Microsoft.Ajax.Utilities;
using OnlineJudge.FormModels;
using OnlineJudge.Models;
using OnlineJudge.ResponseModels;
using OnlineJudge.Services;

namespace OnlineJudge.Repository {
    public partial class ProblemRepository {

        public OjDBContext context;

        public ProblemRepository(){
            this.context = new OjDBContext();
        }

        public ProblemRepository(OjDBContext context){
            this.context = context;
        }


        private Problem FindProblemById(int id){
            var problem = context.Problems.Find(id);

            if (problem == null){
                throw new ObjectNotFoundException();
            }

            return problem;
        }

        // get all problems in descending order of their creation
        public IQueryable<Problem> GetAllProblemsList(){
            var problems = from problem in context.Problems
                orderby problem.CreateDate
                descending
                select problem;

            return problems;
        }

        public ProblemDetails GetProblemDetails(int id){
            var problem = FindProblemById(id);

            return new ProblemDetails(problem);
        }

        public MemoryStream GetProblemInputTestCases(int id){
            var problem = FindProblemById(id);

            MemoryStream data_stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(data_stream);

            // try write line async 
            writer.WriteLine(problem.TestCaseInput);
            writer.Flush();
            writer.Close();

            return data_stream;
        }


        public MemoryStream GetProblemOutputTestCases(int id){
            var problem = FindProblemById(id);

            MemoryStream data_stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(data_stream);

            // try write line async 
            writer.WriteLine(problem.TestCaseOutput);
            writer.Flush();
            writer.Close();

            return data_stream;
        }

        public void CreateProblem(ProblemCreationForm data){
            Problem problem = new Problem(){
                Title = data.Title,
                Description = data.Description,
                Constraints = data.Constraints,
                InputSpecification = data.InputSpecification,
                OutputSpecification = data.OutputSpecification,
                SampleInput = data.SampleInput,
                SampleOutput = data.SampleOutput,
                Notes = data.Notes,
                TimeLimit = data.TimeLimit,
                MemoryLimit = data.MemoryLimit,
                IsPublic = data.IsPublic,
                TestCaseInput = data.TestCaseInput,
                TestCaseOutput = data.TestCaseOutput,

                CreateDate = DateTime.Now,
                // todo set to the request sender
                Creator = context.Users.Find(1),
            };

            context.Problems.Add(problem);
            context.SaveChanges();
        }

        public void DeleteProblem(int id){
            Problem prob = context.Problems.Find(id);
            if (prob == null){
                throw new ObjectNotFoundException("Problem record with specified id does not exist");
            }

            context.Problems.Remove(prob);
            context.SaveChanges();
        }

        public void UpdateProblem(int id, ProblemCreationForm problem_form){
            var problem = context.Problems.Find(id);
            if (problem == null){
                throw new ObjectNotFoundException("Problem record with specified id does not exist");
            }

            problem.Title = problem_form.Title;
            problem.Description = problem_form.Description;
            problem.Constraints = problem_form.Constraints;
            problem.InputSpecification = problem_form.InputSpecification;
            problem.OutputSpecification = problem_form.OutputSpecification;
            problem.SampleInput = problem_form.SampleInput;
            problem.SampleOutput = problem_form.SampleOutput;
            problem.Notes = problem_form.Notes;
            problem.TimeLimit = problem_form.TimeLimit;
            problem.MemoryLimit = problem_form.MemoryLimit;
            problem.IsPublic = problem_form.IsPublic;


            // these values will be updated only if they were provided
            if(problem_form.TestCaseInput != null){
                problem.TestCaseInput = problem_form.TestCaseInput;
            }
            if(problem_form.TestCaseOutput != null){
                problem.TestCaseOutput = problem_form.TestCaseOutput;
            }
            
            
            context.SaveChanges();
        }

        public Submission CreateSubmission(SubmissionFormData submission_data){
            int problem_code = Int32.Parse(submission_data.ProblemCode);
            var judge = new JudgeService();
            var problem = context.Problems.Find(problem_code);


            var submission = new Submission(){
                Status = context.SubmissionStatus.Find(Verdict.Running),
                Problem = problem,
                SourceCode = submission_data.SolutionCode,
                SubmissionDate = DateTime.Now,
                Submitter = context.Users.First(x => x.UserName == "admin")
            };

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

        public void UpdateSubmissionStatus(int submission_id, Verdict status){
            Submission submission = context.Submissions.Find(submission_id);

            if (submission == null){
                throw new ObjectNotFoundException("Submission with specified id not found");
            }

            submission.Status = context.SubmissionStatus.Find(status);
            context.SaveChanges();
        }


        public int GetProblemCount(){
            return context.Problems.Count();
        }

        public int GetSubmissionCount(){
            return context.Submissions.Count();
        }

        public int GetSubmissionCountOfUser(int user_id){
            return context.Submissions.Count(x=>x.Submitter.Id == user_id);
        }

        public int GetSubmissionCountOfProblem(int problem_id){
            return context.Submissions.Count(x=>x.Problem.Id == problem_id);
        }
    }
}