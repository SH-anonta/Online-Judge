using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;

namespace OnlineJudge.FormModels {
    public class ProblemCreationForm {

        public string Title { set; get; }
        public string Description { set; get; }
        public string Constraints { set; get; }
        
        public string InputSpecification { set; get; }
        public string OutputSpecification { set; get; }
        
        public string SampleInput { set; get; }
        public string SampleOutput { set; get; }
        
        public string Notes { set; get; }
        
        public double TimeLimit { set; get; }
        public double MemoryLimit { set; get; }

        public string TestCaseInput { set; get; }
        public string TestCaseOutput { set; get; }

        public bool IsPublic { set; get; }

        public ProblemCreationForm(NameValueCollection data, ICollection<MultipartFileData> files){
            Trace.WriteLine("Method was called");

            this.Title  = data.GetValues("Title")[0];
            this.Description  = data.GetValues("Description")[0];
            this.Constraints  = data.GetValues("Constraints")[0];
            this.InputSpecification  = data.GetValues("InputSpecification")[0];
            this.OutputSpecification  = data.GetValues("OutputSpecification")[0];
            this.SampleInput  = data.GetValues("SampleInput")[0];
            this.SampleOutput  = data.GetValues("SampleOutput")[0];
            this.Notes  = data.GetValues("Notes")[0];
            this.TimeLimit  = Double.Parse(data.GetValues("TimeLimit")[0]);
            this.MemoryLimit  = Double.Parse(data.GetValues("MemoryLimit")[0]);
            this.IsPublic  = Boolean.Parse(data.GetValues("IsPublic")[0]);


            this.TestCaseInput = null;
            this.TestCaseOutput = null;

            // only if files are provided 
            if (files.Count == 2){
                // todo which file contains which data is determined by the order of files, find a better way to do this 
                string input_file_path = files.First().LocalFileName;
                string output_file_path = files.Skip(1).First().LocalFileName;

                this.TestCaseInput = File.ReadAllText(input_file_path);
                this.TestCaseOutput = File.ReadAllText(output_file_path);
            }

        }
    }
}