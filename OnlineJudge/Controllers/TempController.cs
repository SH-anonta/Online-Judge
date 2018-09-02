
using System.Web;
using System.Web.Http;

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
        public IHttpActionResult Problem(){

            return Ok("AAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        }

        

        [HttpGet]
        [Route("session-get")]
        public IHttpActionResult SessionTest(int val){
            if (val == 0){
                var a = HttpContext.Current.Session["A"];
                return Ok(a);
            }
            else{
                HttpContext.Current.Session["A"] = val;
                return Ok();
            }
            
        }

        class MyClass{
            public string AB { set; get; }
            public string AC { set; get; }
            public string AD { set; get; }

            public MyClass(){
                AB = "abcd";
                AC = "abcxxxxd";
                AD = "xxxabcd";
            }
        }

        [HttpGet]
        [Route("session-comp")]
        public IHttpActionResult SesisonComp(int val){
            if (val == 0){
                var a = HttpContext.Current.Session["A"];
                return Ok(a);
            }
            else{
                HttpContext.Current.Session["A"] = new MyClass();
                return Ok();
            }
            
        }

        [HttpGet]
        [Route("session")]
        public IHttpActionResult GetSession(int val){
            return Ok(HttpContext.Current.Session["LoginInfo"]);

        }

        
        
    }
}
