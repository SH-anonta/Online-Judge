using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineJudge.Models {
    public class Announcement {
        public int Id{ set; get; }

        public string Title{ set; get; }
        public string Description{ set; get; }
        
        public virtual User Creator{ set; get; }
        
        public DateTime CreateDate{ set; get; }
    }
}