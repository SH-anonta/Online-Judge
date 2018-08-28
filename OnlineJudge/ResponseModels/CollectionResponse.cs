using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineJudge.ResponseModels {
    public class CollectionResponse {
        public int TotalCount { set; get; }
        public IEnumerable<Object> Collection { set; get; }
    }
}