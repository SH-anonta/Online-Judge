using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineJudge.ResponseModels {
    // common response object for all controllers
    public class ResponseMessage{
        public bool Success { get; set; }
        public Object[] Data { get; set; }
        public String[] Errors { get; set; }
    }

    
}