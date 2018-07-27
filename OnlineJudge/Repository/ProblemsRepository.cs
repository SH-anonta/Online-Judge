using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.WebPages;
using Microsoft.Ajax.Utilities;
using OnlineJudge.FormModels;
using OnlineJudge.Models;
using OnlineJudge.ResponseModels;

namespace OnlineJudge.Repository {
    public partial class DataRepository {

        private static Problem FindProblemById(int id){
            var ctx = getContext();
            var problem = ctx.Problems.Find(id);

            if (problem == null){
                throw new ObjectNotFoundException();
            }

            return problem;
        }

        // get all problems in descending order of their creation
        public static List<ProblemListItem> GetProblemList(){
            var ctx = getContext();

            var problems = from problem in ctx.Problems
                orderby problem.CreateDate
                descending
                select problem;

            return ProblemListItem.MapTo(problems);
        }

        public static ProblemDetails GetProblemDetails(int id){
            var problem = FindProblemById(id);

            return new ProblemDetails(problem);
        }

        public static MemoryStream GetProblemInputTestCases(int id){
            var problem = FindProblemById(id);

            MemoryStream data_stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(data_stream);

            // try write line async 
            writer.WriteLine(problem.TestCaseInput);
            writer.Flush();
            writer.Close();

            return data_stream;
        }


        public static MemoryStream GetProblemOutputTestCases(int id){
            var problem = FindProblemById(id);

            MemoryStream data_stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(data_stream);

            // try write line async 
            writer.WriteLine(problem.TestCaseOutput);
            writer.Flush();
            writer.Close();

            return data_stream;
        }

        public static void CreateProblem(ProblemCreationForm data){
            var ctx = getContext();

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
                Creator = ctx.Users.Find(1),
            };

            ctx.Problems.Add(problem);
            ctx.SaveChanges();
        }

        public static void DeleteProblem(int id){
            var ctx = getContext();
            Problem prob = ctx.Problems.Find(id);
            if (prob == null){
                throw new ObjectNotFoundException("Problem record with specified id does not exist");
            }

            ctx.Problems.Remove(prob);
            ctx.SaveChanges();
        }

        public static void UpdateProblem(int id, ProblemCreationForm problem_form){
            var ctx = getContext();
            var problem = ctx.Problems.Find(id);
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
            
            
            ctx.SaveChanges();
        }
    }
}