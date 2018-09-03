using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineJudge.Models
{
    public class SystemLog
    {
        public int Id { get; set; }
        public DateTime Date{ get; set; }
        public int Thread { get; set; }
        public string Level { get; set; }
        public string Logger { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
    }
}