using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineJudge.FormModels {
    public class UserRegistrationFormData : IFormData {
        public string UserName { set; get; }
        public string Email { set; get; }
        public string Password { set; get; }
        public string ConfirmPassword { set; get; }


        public FormDataValidationResult Validate(){
            throw new NotImplementedException();
            return null;
        }
    }


}