using System;
using System.Linq;
using System.Web;
using OnlineJudge.Models;
using OnlineJudge.Repository;
using Login = System.Web.UI.WebControls.Login;

namespace OnlineJudge.Services {
    

    public class UserLoginInfo{
        public string UserName { get; }
        public int UserId{ get; }
        public UserTypeEnum UserType { get; }
        public DateTime LogInTime { get; }

        public UserLoginInfo(User user){
            UserName = user.UserName;
            UserId = user.Id;
            UserType = user.UserType.Id;
            LogInTime = DateTime.Now;
        }

        public bool IsAdmin(){
            return UserType == UserTypeEnum.Admin;
        }

        public bool IsJudge(){
            return UserType == UserTypeEnum.Judge;
        }

    }



    public partial class UserService {
        private static readonly string LOGIN_INFO_SESSION_KEY = "LoginInfo";
        
        private UserLoginInfo login_info;
        private UserRepository user_repository;

        public UserService(){
            user_repository = new UserRepository();

            try{
                // if the user is not authenticated, this will evaluate to null
                login_info = (UserLoginInfo) HttpContext.Current.Session[LOGIN_INFO_SESSION_KEY];
            }
            catch (NullReferenceException ex){
                // intentionally do nothing
            }
            
        }

        public int GetUserId(){
            if (login_info == null){
                throw new Exception("User is not authenticated");
            }

            return login_info.UserId;
        }


        public bool UserIsAuthenticated(){
            return login_info != null;
        }

        // logs in the user, and returns wether the login was successful
        public bool LoginUser(string UserName, string Password){
            bool result = user_repository.IsValidUser(UserName, Password);

            if (result){
                var user = user_repository.GetUserByUserName(UserName);
                login_info = new UserLoginInfo(user);
                SetLoginDataOnSession(login_info);
            }

            return result;
        }

        public void LogoutUser(){
            login_info = null;
            RemoveLoginDataOnSession();
        }

        public UserLoginInfo GetUserState(){
            return login_info;
        }

        // session handler

        private void SetLoginDataOnSession(UserLoginInfo info){
            HttpContext.Current.Session[LOGIN_INFO_SESSION_KEY] = info;
        }

        private void RemoveLoginDataOnSession(){
            HttpContext.Current.Session[LOGIN_INFO_SESSION_KEY] = null;
        }

        
    }


    // authorization methods go here 
    public partial class UserService{
        // User profile

        // either uesr is admin or is editing their own user profile
        public bool IsAuthorizedToEditUesrProfile(int user_id){
            if (!UserIsAuthenticated()){
                return false;
            }

            if (login_info.IsAdmin()){
                return true;
            }

            return login_info.UserId == user_id;
        }

        public bool IsAuthorizedToViewUserProblems(int user_id){
            if (!UserIsAuthenticated()){
                return false;
            }

            if (login_info.IsAdmin()){
                return true;
            }

            return login_info.UserId == user_id;
        }


        // Announcement
        public bool IsAuthorizedToCreateAnnouncements(){
            return UserIsAuthenticated() && login_info.IsAdmin();
        }

        public bool IsAuthorizedToDeleteAnnouncements(){
            return UserIsAuthenticated() && login_info.IsAdmin();
        }

        public bool IsAuthorizedToEditAnnouncements(int announcement_id){
            return UserIsAuthenticated() && login_info.IsAdmin();
        }

        // Problems 
        public bool IsAuthorizedToCreateProblem()
        {
            return UserIsAuthenticated() &&
                   (login_info.UserType == UserTypeEnum.Admin
                    || login_info.UserType == UserTypeEnum.Judge);
        }

        public bool IsAuthorizedToViewProblem(int problem_id){
            if (!UserIsAuthenticated()){
                return false;
            }

            if (login_info.IsAdmin()){
                return true;
            }

            // else check if the user is a judge and is the creator of this problem
            var context = new OjDBContext();
            var problem = context.Problems.Find(problem_id);

            return problem.Creator.Id == login_info.UserId;
        }

        public bool IsAuthorizedToEditProblem(int problem_id){
            if (!UserIsAuthenticated()){
                return false;
            }

            if (login_info.IsAdmin()){
                return true;
            }

            // else check if the user is a judge and is the creator of this problem
            var context = new OjDBContext();
            var problem = context.Problems.Find(problem_id);

            return login_info.IsJudge() && problem.Creator.Id == login_info.UserId;

        }

        public bool IsAuthorizedToDeleteProblem(int problem_id){
            // same as checking authorization for problem editing
            return IsAuthorizedToEditProblem(problem_id);
        }

        public bool IsAuthorizedToAccessProblemTestCaseFiles(int problem_id){
            return IsAuthorizedToEditProblem(problem_id);
        }
        
        public bool IsAuthorizedToSubmitToProblem(int problem_id){
            var context = new OjDBContext();

            // either user is the creator of problem, admin, or the problem is publicly visible
            return IsAuthorizedToEditProblem(problem_id) || context.Problems.Find(problem_id).IsPublic;
        }

        public bool IsAuthorizedToViewAllProblemList(){
            return UserIsAuthenticated() && login_info.IsAdmin();
        }

        // Contests
        public bool IsAuthorizedToCreateContest(){
            return UserIsAuthenticated() && (login_info.IsAdmin() || login_info.IsJudge());
        }

        public bool IsAuthorizedToEditContest(int contest_id){
            if (!UserIsAuthenticated()){
                return false;
            }

            if (login_info.IsAdmin()){
                return true;
            }

            // else check if the user is a judge and is the creator of this problem
            var context = new OjDBContext();
            var contest = context.Contests.Find(contest_id);

            return contest.Creator.Id == login_info.UserId;
        }

        public bool IsAuthorizedToViewContestProblem(int contest_id){
            if (!UserIsAuthenticated()){
                return false;
            }

            if (IsAuthorizedToEditContest(contest_id)){
                return true;
            }

            // or if the contest has already started (possibly ended too) and the user is 
            var context = new OjDBContext();
            var contest = context.Contests.Find(contest_id);
            return contest.StartDate < DateTime.Now;
        }

        public bool IsAuthorizedToViewContestProblems(int contest_id){
            // same logic
            return IsAuthorizedToViewContestProblem(contest_id);
        }

        public bool IsAuthorizedToDeleteContest(int contest_id){
            // same logic
            return IsAuthorizedToEditContest(contest_id);
        }

        public bool IsAuthorizedToSubmitToContest(int contest_id){
            if (!UserIsAuthenticated()){
                return false;
            }

            if (IsAuthorizedToEditContest(contest_id)){
                return true;
            }

            var context = new OjDBContext();
            var contest = context.Contests.Find(contest_id);
            return contest.StartDate < DateTime.Now &&
                   context.Contestants.FirstOrDefault(x =>
                       x.User.Id == login_info.UserId && x.Contest.Id == contest_id) != null;
        }


    }

}

