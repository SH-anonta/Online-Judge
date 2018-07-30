using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages;
using OnlineJudge.Models;

namespace OnlineJudge.FormModels {
    public class AnnouncementFormData : IFormData{
        public string Title{ set; get; }
        public string Description{ set; get; }

        public FormDataValidationResult Validate(){
            FormDataValidationResult result = new FormDataValidationResult();

            if (Title == null || Title.IsEmpty()){
                result.AddErrorMessage("Title is required");
            }

            if (Description == null || Description.IsEmpty()){
                result.AddErrorMessage("Description is required");
            }

            return result;
        }
    }

    
}