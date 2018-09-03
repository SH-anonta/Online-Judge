using log4net;
using OnlineJudge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineJudge.Repository {
    public class LogRepository {
        private OjDBContext context = new OjDBContext();
        // returns all logs in reverse chronological order
        public IQueryable<SystemLog> GetAllLogs(){
            var logs = context.SystemLogs.OrderByDescending(x => x.Id);

            return logs;
        }

        // returns all logs in reverse chronological order
        public IQueryable<SystemLog> GetLogs(int from, int to){
            // use skip() and take to select the range specified 
            var logs = context.SystemLogs.OrderByDescending(x => x.Id);

            return logs.Skip(from - 1).Take(to - from + 1);
        }

        //returns all logs in reverse chronological order as per their dates.
        // Flag enum givens us how much of the past logs we want to see
        public IQueryable<SystemLog> GetLogs(Recent flag)
        {
            if (flag == Recent.Last_7_Days) { return context.SystemLogs.OrderByDescending(x => x.Id).Where(x => new TimeSpan((DateTime.Now - x.Date).Ticks).Minutes <= 10080); }
            else if (flag == Recent.Last_30_Days) { return context.SystemLogs.OrderByDescending(x => x.Id).Where(x => new TimeSpan((DateTime.Now - x.Date).Ticks).Minutes <= 43200); }
            else { return context.SystemLogs.OrderByDescending(x => x.Id); }
        }
    }
}
