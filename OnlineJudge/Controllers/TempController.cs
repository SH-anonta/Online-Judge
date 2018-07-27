using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using OnlineJudge.FormModels;
using OnlineJudge.Models;

namespace OnlineJudge.Controllers
{
    [RoutePrefix("api/temp")]
    public class TempController : ApiController{
        private static readonly log4net.ILog logger =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [Route("")]
        [HttpGet]
        public string Temp(){
            logger.Info("Request recieved");
            return "temp";
        }

        [Route("problem")]
        [HttpGet]
        public string Problem(){
            var ctx = new OjDBContext();

            ctx.Problems.Add(new Problem()
            {
                Title = "heloo",
                TestCaseInput = "sdfasfdas",
                TestCaseOutput = "sdfasfasd"
            });
            ctx.SaveChanges();

            return "Problem: " + ctx.Problems.Count();
        }

        public class Notice{
            public string Title { set; get; }
            public string Descripiton { set; get; }
        }

        

    }
}
