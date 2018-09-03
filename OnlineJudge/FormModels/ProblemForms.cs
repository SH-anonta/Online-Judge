using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace OnlineJudge.FormModels {
    public class ProblemCreationForm : IFormData{

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

            Title  = data.GetValues("Title")[0];
            Description  = data.GetValues("Description")[0];
            Constraints  = data.GetValues("Constraints")[0];
            InputSpecification  = data.GetValues("InputSpecification")[0];
            OutputSpecification  = data.GetValues("OutputSpecification")[0];
            SampleInput  = data.GetValues("SampleInput")[0];
            SampleOutput  = data.GetValues("SampleOutput")[0];
            Notes  = data.GetValues("Notes")[0];
            TimeLimit  = Double.Parse(data.GetValues("TimeLimit")[0]);
            MemoryLimit  = Double.Parse(data.GetValues("MemoryLimit")[0]);
            IsPublic  = Boolean.Parse(data.GetValues("IsPublic")[0]);


            TestCaseInput = null;
            TestCaseOutput = null;

            // only if files are provided 
            if (files.Count == 2){
                // todo which file contains which data is determined by the order of files, find a better way to do this 
                string input_file_path = files.First().LocalFileName;
                string output_file_path = files.Skip(1).First().LocalFileName;

                this.TestCaseInput = File.ReadAllText(input_file_path);
                this.TestCaseOutput = File.ReadAllText(output_file_path);
            }

        }

        public FormDataValidationResult Validate(){
            var result = new FormDataValidationResult();

            
            if(Title.IsNullOrWhiteSpace()){
                result.AddErrorMessage("Title is required");
            }
            if(Description.IsNullOrWhiteSpace()){
                result.AddErrorMessage("Description is required");
            }
            if(Constraints.IsNullOrWhiteSpace()){
                result.AddErrorMessage("Constraints is required");
            }
            if(InputSpecification.IsNullOrWhiteSpace()){
                result.AddErrorMessage("InputSpecification is required");
            }
            if(OutputSpecification.IsNullOrWhiteSpace()){
                result.AddErrorMessage("OutputSpecification is required");
            }
            if(SampleInput.IsNullOrWhiteSpace()){
                result.AddErrorMessage("SampleInput is required");
            }
            if(SampleOutput.IsNullOrWhiteSpace()){
                result.AddErrorMessage("SampleOutput is required");
            }
            if(Notes.IsNullOrWhiteSpace()){
                result.AddErrorMessage("Notes is required");
            }
            if(TimeLimit == 0 || TimeLimit > 20){
                result.AddErrorMessage("TimeLimit is invalid");
            }
            if(MemoryLimit == 0 || MemoryLimit > 512){
                result.AddErrorMessage("MemoryLimit is invalid");
            }
            if(string.IsNullOrEmpty(TestCaseInput)){
                result.AddErrorMessage("TestCaseInput is required");
            }
            if(string.IsNullOrEmpty(TestCaseOutput)){
                result.AddErrorMessage("TestCaseOutput is required");
            }

            return result;
        }
    }
}