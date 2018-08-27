using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineJudge.Controllers{
    [RoutePrefix("admin-site")]
    public class AdminController : Controller{
        [HttpGet]
        [Route("")]
        public ActionResult Index(){
            return View("AdminHome");
        }
        

    }


}