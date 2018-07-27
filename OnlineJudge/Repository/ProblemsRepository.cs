using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.IO;
using System.Linq;
using System.Web;
using OnlineJudge.ResponseModels;

namespace OnlineJudge.Repository {
    public partial class DataRepository {
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
            var ctx = getContext();

            var problem = ctx.Problems.Find(id);

            if (problem == null){
                throw new ObjectNotFoundException();
            }
            

            return new ProblemDetails(problem);
        }

        public static MemoryStream GetProblemInputTestCases(int id){
            var ctx = getContext();
            var problem = ctx.Problems.Find(id);

            if (problem == null){
                throw new ObjectNotFoundException();
            }

            MemoryStream data_stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(data_stream);

            // try write line async 
            writer.WriteLine(problem.TestCaseInput);
            writer.Flush();
            writer.Close();

            return data_stream;
        }
    }
}