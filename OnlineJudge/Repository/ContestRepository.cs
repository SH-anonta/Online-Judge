using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Diagnostics;
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

        public IEnumerable<Contest> GetAllContestsList(){
            return from contest in context.Contests
                orderby contest.StartDate
                select contest;
        }

        public IEnumerable<Contest> GetRecentContestsList(int from, int to){
            var contests = GetAllContestsList();
            return contests.Skip(from - 1).Take(to-from+1);
        }

        public Contest GetContestById(int contest_id){
            Contest contest = context.Contests.Include(x => x.Problems).FirstOrDefault(x => x.Id== contest_id);

            if (contest == null){
                throw new ObjectNotFoundException("Contest entry with specified id not found");
            }

            return contest;
        }

        public Contestant RegisterUserForContest(int contest_id, int user_id){
            // todo implemet with, check if user is already registered

            Contest contest = context.Contests.Include(x=> x.Contestants).FirstOrDefault(x => x.Id == contest_id);
            
            var user = context.Users.Find(user_id);

            if (contest == null){
                throw new ObjectNotFoundException("Contest with specified id not found");
            }

            if (user == null){
                throw new ObjectNotFoundException("User with specified id not found");
            }

            // if user is already registered for this contest
            if (contest.Contestants.Count(x => x.User == user) > 0){
                throw new InvalidOperationException("User is already registered for contest");
            }

            Trace.WriteLine(contest.Contestants.Count);
            var contestant = new Contestant(user);
            
            contest.Contestants.Add(contestant);
            context.SaveChanges();
            
            return contestant;
        }
    }
}