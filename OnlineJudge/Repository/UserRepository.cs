using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using OnlineJudge.FormModels;
using OnlineJudge.Models;
using OnlineJudge.ResponseModels;

namespace OnlineJudge.Repository {
    public partial class DataRepository {

        public List<UserListItemData> GetUserList(){

            return UserListItemData.MapTo(context.Users);
        }


        public UserDetailsData GetUserDetails(int id){
            User user = context.Users.Find(id);

            if (user == null){
                throw new ObjectNotFoundException("User with specified ID not found");
            }

            return new UserDetailsData(user);
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
    }
}