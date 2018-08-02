using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using OnlineJudge.FormModels;
using OnlineJudge.Repository;
using OnlineJudge.ResponseModels;

namespace OnlineJudge.Controllers{
    [RoutePrefix("api/users")]
    public class UserController : ApiController{
        public UserRepository user_repository= new UserRepository();


        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult UserDetails(int id){

            try{
                return Ok(new UserDetailsData(user_repository.GetUserDetails(id)));
            }
            catch (ObjectNotFoundException e){
                return NotFound();
            }
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult UserList(){
            return Ok(UserListItemData.MapTo(user_repository.GetUserList()));
        }

        [HttpPost]
        [Route("{id}/edit")]
        public IHttpActionResult UpdateUser([FromBody]UserProfileUpdateForm data, int id){
            try{
                user_repository.UpdateUser(id, data);
                return Ok();
            }
            catch (ObjectNotFoundException e){
                return NotFound();
            }
        }

    }
}
