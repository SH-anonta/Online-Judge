using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineJudge.FormModels {
    public class ContestCreationFormData: IFormData{
        public int Id{ set; get; }
        public string Title { set; get; }
        public string Description { set; get; }
        
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
        
        // list of problem id's to be included
        public List<int> Problems { set; get; }

        public FormDataValidationResult Validate(){
            throw new NotImplementedException();
            return null;
        }
    }

    

}