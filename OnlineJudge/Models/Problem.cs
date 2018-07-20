using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineJudge.Models {
    public class Problem {
        public int Id{ set; get; }

        public string Title { set; get; }

        public string TestCaseInput { set; get; }
        public string TestCaseOutput { set; get; }
    }
}