using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Diagnostics;
using System.Linq;
using System.Web;
using JudgeCodeRunner;
using OnlineJudge.FormModels;
using OnlineJudge.Models;
using OnlineJudge.ResponseModels;

namespace OnlineJudge.Repository {
    public partial class UserRepository {

        public OjDBContext context;

        public UserRepository(){
            this.context = new OjDBContext();
        }

        public UserRepository(OjDBContext context){
            this.context = context;
        }

        public IEnumerable<User> GetUserList(){
            return context.Users.Select(x=>x);
        }

        public IEnumerable<Submission> GetUserSubmissions(int user_id, int start, int limit){
            var submissions = context.Submissions
                                .Where(x => x.Submitter.Id == user_id)
                                .OrderByDescending(x=>x.SubmissionDate);

            return submissions.Skip(start-1).Take(limit-start+1);;
        }

        public User GetUserDetails(int id){
            User user = context.Users.Find(id);

            if (user == null){
                throw new ObjectNotFoundException("User with specified ID not found");
            }

            return user;
        }

        public void UpdateUser(int id, UserProfileUpdateForm data){
            User user = context.Users.Find(id);

            if (user == null){
                throw new ObjectNotFoundException("User with specified ID not found");
            }

            user.Email = data.Email;
            user.Password = data.Password;
            user.UserType = context.UserTypes.Find(data.UserType);

            context.SaveChanges();
        }

        public IEnumerable<Contest> GetUserContestHistory(int user_id){
            return context.Contestants.Where(x => x.User.Id == user_id).Select(x=>x.Contest);
        }

        public void CreateNewUser(UserRegistrationFormData data){
            context.Users.Add(new User(){
                UserName = data.UserName,
                Email = data.Email,
                Password = data.Password, // todo hash first
                UserType = context.UserTypes.Find(UserTypeEnum.User)
            });

            context.SaveChanges();
        }

        public int GetUserCount(){
            return context.Users.Count();
        }

        public int GetUserSubmissionCount(int uesr_id){
            return context.Submissions.Count(x=>x.Submitter.Id == uesr_id);
        }

        public IEnumerable<Problem> GetProblems(int user_id, int start, int limit){
            var problems = context.Problems.Where(x => x.Creator.Id == user_id).OrderByDescending(x=>x.CreateDate);
            return problems.Skip(start-1).Take(limit-start+1);
        }

        public int GetProblemCount(int user_id){
            return context.Problems.Count(x => x.Creator.Id == user_id);
        }
    }
}