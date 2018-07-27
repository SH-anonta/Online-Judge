using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
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
    }
}