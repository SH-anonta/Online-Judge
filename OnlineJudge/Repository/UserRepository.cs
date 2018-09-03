using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using OnlineJudge.FormModels;
using OnlineJudge.Models;

namespace OnlineJudge.Repository {
    class HashUtility{
        public static string MD5Hash(string input){
            // Use input string to calculate MD5 hash
            using (MD5 md5 = MD5.Create()){
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++){
                    sb.Append(hashBytes[i].ToString("X2"));
                }

                return sb.ToString();
            }
        }
    }

    public partial class UserRepository {

        public OjDBContext context;

        public UserRepository(){
            this.context = new OjDBContext();
        }

        public UserRepository(OjDBContext context){
            this.context = context;
        }

        public bool IsValidUser(string userName, string password){
            User user;
            password = HashUtility.MD5Hash(password);
            user = context.Users.Where(x => x.UserName == userName && x.Password == password )
                .SingleOrDefault();
            return (user != null) ? true : false;
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
            var contests = context.Contestants.Where(x => x.User.Id == user_id).Select(x=>x.Contest);
            return contests;
        }

        public void CreateNewUser(UserRegistrationFormData data){
            context.Users.Add(new User(){
                UserName = data.UserName,
                Email = data.Email,
                Password = HashUtility.MD5Hash(data.Password),
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

        public IEnumerable<UserType> GetUserTypes(){
            return context.UserTypes.Select(x => x);
        }

        public User GetUserByUserName(string userName){
            return context.Users.FirstOrDefault(x => x.UserName == userName);
        }
    }
}