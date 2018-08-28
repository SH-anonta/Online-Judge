using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineJudge.Models
{
    public class UserTest
    {
        public int Id { set; get; }
        public string UserName { set; get; }
        public string Title { set; get; }
        public string Email { set; get; }
        public string Password { set; get; }
        public string Usertype { set; get; }
    }
}