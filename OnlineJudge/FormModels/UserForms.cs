using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineJudge.Models;

namespace OnlineJudge.FormModels {
    public class UserProfileUpdateForm{
        public string Email { set; get; }
        public string Password { set; get; }
        public string ConfirmPassword { set; get; }
        public UserTypeEnum  UserType { set; get; }
    }
}