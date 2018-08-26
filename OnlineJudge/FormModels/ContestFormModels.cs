using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace OnlineJudge.FormModels {
    public class ContestCreationFormData: IFormData{
        public string Title { set; get; }
        public string Description { set; get; }

        public string Password { set; get; }
        public string ConfirmPassword { set; get; }
        
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }

        public bool IsPublic { set; get; }
        
        
        // list of problem id's to be included
        public IEnumerable<int> Problems { set; get; }

        public FormDataValidationResult Validate(){
            var result = new FormDataValidationResult();

            if (Title.IsNullOrWhiteSpace()){
                result.AddErrorMessage("Title is required");
            }

            if (Description.IsNullOrWhiteSpace()){
                result.AddErrorMessage("Description is required");
            }

            if (!IsPublic){
                if (Password.IsNullOrWhiteSpace()){
                    result.AddErrorMessage("Password is required");
                }

                if (ConfirmPassword.IsNullOrWhiteSpace()){
                    result.AddErrorMessage("Confirm password is required");
                }
                else if (ConfirmPassword != Password){
                    result.AddErrorMessage("Passwords do not match");
                }
            }

            if (StartDate < DateTime.Now){
                result.AddErrorMessage("Invalid Start date");
            }

            if (StartDate >= EndDate){
                result.AddErrorMessage("Invalid start and end date");
            }

            if (Problems.ToList().Count == 0){
                result.AddErrorMessage("1 or more problems must be added");
            }

            // todo check problem uniqueness

            return result;
        }
    }

    

}