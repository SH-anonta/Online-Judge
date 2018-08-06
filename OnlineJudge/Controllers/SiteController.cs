using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OnlineJudge.FormModels;
using OnlineJudge.Models;
using OnlineJudge.ResponseModels;

namespace OnlineJudge.Controllers{


    [RoutePrefix("api")]
    public class SiteController : ApiController{

        [Route("programming-languages")]
        [HttpGet]
        public IHttpActionResult ProgrammingLanguages(SubmissionFormData data){
            var context = new OjDBContext();
            IEnumerable<ProgrammingLanguage> languages = context.ProgrammingLanguages.OrderBy(x=>x.Name);
            
            return Ok(ProgrammingLanguageData.MapTo(languages));
        }
    }
}
