using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineJudge.Models {
    public class User {
        public int Id{set; get;}
        
        public string UserName{set; get;}
        public string Email{set; get;}
        public string Password{set; get;}
        
        // todo add user type
//        public virtual UserType UserType{set; get;}
    }

    public class UserType {
        public int Id{set; get;}
        public string TypeName{set; get;}
    }
}