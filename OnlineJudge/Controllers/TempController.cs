﻿using System;
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
using OnlineJudge.Repository;

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

        [Route("page")]
        [HttpGet]
        public IHttpActionResult Problem(int? a, int? b){
            return Ok(a + b);
        }

        public class Notice{
            public string Title { set; get; }
            public string Descripiton { set; get; }
        }

        
        
    }
}
