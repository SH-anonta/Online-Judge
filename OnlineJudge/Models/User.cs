﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineJudge.Models {
    public class User {
        public int Id{set; get;}
        
        public string UserName{set; get;}
        public string Email{set; get;}
        public string Password{set; get;}
        
        public virtual UserType UserType{set; get;}
        public ICollection<Submission> Submissions { set; get; }
    }

    public enum UserTypeEnum{
        Admin, User, Judge
    }

    public class UserType {
        public UserTypeEnum Id{set; get;}
        public string TypeName{set; get;}
    }
}