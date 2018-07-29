using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using OnlineJudge.Repository;

namespace OnlineJudge.Controllers{
    [RoutePrefix("api/users")]
    public class UserController : ApiController{
        public DataRepository data_repository= new DataRepository();


        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult UserDetails(int id){

            try{
                return Ok(data_repository.GetUserDetails(id));
            }
            catch (ObjectNotFoundException e)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult UserList(){
            return Ok(data_repository.GetUserList());
        }

        [HttpPost]
        [Route("{id}/edit")]
        public IHttpActionResult UpdateUser(int id){
            // todo implement
            return Ok();
        }

    }
}
