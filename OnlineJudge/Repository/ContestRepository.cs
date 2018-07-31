using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineJudge.Models;

namespace OnlineJudge.Repository {
    public class ContestRepository{
        private OjDBContext context;

        public ContestRepository(){
            this.context= new OjDBContext();
        }

        public ContestRepository(OjDBContext context){
            this.context= context;
        }

        public IEnumerable<Contest> GetAllContestList(){
            return from contest in context.Contests
                orderby contest.StartDate
                select contest;
        }

        public IEnumerable<Contest> GetAllContestList(int from, int to){
            var contests = GetAllContestList();
            return contests.Skip(from - 1).Take(to-from+1);
        }
    }
}