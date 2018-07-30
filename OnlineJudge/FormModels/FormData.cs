using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineJudge.FormModels {
    public class FormDataValidationResult{
        private bool _IsValid = false;

        public bool IsValid
        {
            get { return _IsValid; }
        }

        public List<string> ErrorMessages { get; }

        public FormDataValidationResult(){
            _IsValid = true;
            ErrorMessages = new List<string>();
        }
        
        public void AddErrorMessage(string msg){
            _IsValid = false;
            ErrorMessages.Add(msg);
        }
    }

    public interface IFormData{
        FormDataValidationResult Validate();
    }
}