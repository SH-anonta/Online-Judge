﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using OnlineJudge.Models;

namespace OnlineJudge.ResponseModels {
    public class UserDetailsData {
        public int Id{set; get;}
        public string UserName{set; get;}
        public string Email{set; get;}
        public string UserType{set; get;}
        public int UserTypeId{set; get;}

        public UserDetailsData(User user){
            this.Id = user.Id; 
            this.UserName = user.UserName; 
            this.UserType = user.UserType.TypeName; 
            this.UserTypeId = (int)user.UserType.Id; 
            this.Email = user.Email; 
        }

        public static List<UserDetailsData> MapTo(IEnumerable<User> users){
            var mapped = new List<UserDetailsData>();

            foreach (var user in users){
                mapped.Add(new UserDetailsData(user));
            }

            return mapped;
        }
    }


    public class UserListItemData {
        public int Id{set; get;}
        public string UserName{set; get;}
        public string UserType{set; get;}

        public UserListItemData(User user){
            this.Id = user.Id; 
            this.UserName = user.UserName; 
            this.UserType = user.UserType.TypeName; 
        }

        public static List<UserListItemData> MapTo(IEnumerable<User> users){
            var mapped = new List<UserListItemData>();

            foreach (var user in users){
                mapped.Add(new UserListItemData(user));
            }

            return mapped;
        }
    }

    public class UserTypeData{
        public int Id { set; get; }
        public string Name { set; get; }

        public UserTypeData(UserType data){
            this.Id = (int) data.Id;
            this.Name = data.TypeName;
        }

        public static List<UserTypeData> MapTo(IEnumerable<UserType> users){
            var mapped = new List<UserTypeData>();

            foreach (var user in users){
                mapped.Add(new UserTypeData(user));
            }

            return mapped;
        }
    }
}