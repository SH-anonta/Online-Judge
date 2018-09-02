using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace OnlineJudge.Controllers {
    public class HomeController : Controller {
        [System.Web.Http.Authorize]        
        public ActionResult Index() {
            return View();
        }
        
    }
}