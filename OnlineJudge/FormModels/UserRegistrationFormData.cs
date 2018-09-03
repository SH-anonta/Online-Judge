using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages;
using OnlineJudge.Models;

namespace OnlineJudge.FormModels {
    public class UserRegistrationFormData : IFormData {
        public string UserName { set; get; }
        public string Email { set; get; }
        public string Password { set; get; }
        public string ConfirmPassword { set; get; }


        public FormDataValidationResult Validate(){
            var context = new OjDBContext();
            FormDataValidationResult result = new FormDataValidationResult();

            if (UserName.IsEmpty()){
                result.AddErrorMessage("UserName Required");
            }
            else if (UserName.Length < 4){
                result.AddErrorMessage("UserName is too short");
            }
            else if (context.Users.FirstOrDefault(x=>x.UserName == UserName) != null){
                result.AddErrorMessage("UserName is already taken");
            }

            if (Email.IsEmpty()){
                result.AddErrorMessage("Email is Required");
            }
            else if (!(Email.Contains("@") && Email.Contains("."))){
                result.AddErrorMessage("Email is invalid");
            }

            if (Password.IsEmpty()){
                result.AddErrorMessage("Password must is required");
            }
            else if (Password.Length < 8){
                result.AddErrorMessage("Password must be atleast 8 characters long");
            }
            if (Password != ConfirmPassword){
                result.AddErrorMessage("Passwords do not match");
            }

            return result;
        }
    }


}