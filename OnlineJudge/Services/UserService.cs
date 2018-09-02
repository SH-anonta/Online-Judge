using System;
using System.Web;
using OnlineJudge.Models;
using OnlineJudge.Repository;

namespace OnlineJudge.Services {
    class UserLoginInfo{
        public string UserName { set; get; }
        public int UserId{ set; get; }
        public UserTypeEnum UserType;
        public DateTime LogInTime;

        public UserLoginInfo(User user){
            UserName = user.UserName;
            UserId = user.Id;
            UserType = user.UserType.Id;
            LogInTime = DateTime.Now;
        }
    }

    public class UserService{
        private static readonly string LOGIN_INFO_SESSION_KEY = "LoginInfo";
        
        private UserLoginInfo login_info;
        private UserRepository user_repository;

        public UserService(){
            // if the user is not authenticated, this will evaluate to null
            try
            {
                login_info = (UserLoginInfo) HttpContext.Current.Session["LoginInfo"];
            }
            catch (NullReferenceException ex){
                // intentionally do nothing
            }
            
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

        // session handler

        private void SetLoginDataOnSession(UserLoginInfo info){
            HttpContext.Current.Session[LOGIN_INFO_SESSION_KEY] = info;
        }

        private void RemoveLoginDataOnSession(){
            HttpContext.Current.Session[LOGIN_INFO_SESSION_KEY] = null;
        }
    }


}