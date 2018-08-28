﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineJudge.Models {
    public class Problem {
        public int Id{ set; get; }

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

        public virtual User Creator { set; get; }

        [Column(TypeName = "DateTime2")]
        public DateTime CreateDate { set; get; }
    }
}