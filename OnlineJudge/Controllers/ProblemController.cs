using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using OnlineJudge.FormModels;
using OnlineJudge.Models;
using OnlineJudge.Repository;
using OnlineJudge.ResponseModels;
using OnlineJudge.Services;

namespace OnlineJudge.Controllers{
    [RoutePrefix("api/problems")]
    public class ProblemController : ApiController{
        private static readonly log4net.ILog logger =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        
        // return all problems in order of creation date
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetProblemList(){
            return Ok(DataRepository.GetProblemList());
        }

        [Route("{Id}")]
        [HttpGet]
        public IHttpActionResult GetProblemDetials(int Id){
            try{
                ProblemDetails problem = DataRepository.GetProblemDetails(Id);
                return Ok(problem);
            }
            catch (ObjectNotFoundException e){
                return NotFound();
            }
        }


        private HttpResponseMessage CreateTextFileResponse(MemoryStream mem_stream, string file_name){
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(mem_stream.ToArray())
            };
            response.Content.Headers.ContentDisposition =
                new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment"){
                    FileName = "input.txt"
                };
            response.Content.Headers.ContentType =
                new MediaTypeHeaderValue("text/plain");

            return response;
        }


        [Route("{Id}/input-file")]
        [HttpGet]
        public HttpResponseMessage GetProblemInputTestCases(int Id){
            try{
                MemoryStream mem_stream = DataRepository.GetProblemInputTestCases(Id);
                
                return CreateTextFileResponse(mem_stream, "input.txt");

            }
            catch (ObjectNotFoundException e){
                var resposne = new HttpResponseMessage(HttpStatusCode.NotFound);
                return resposne;
            }
        }

        [Route("{Id}/output-file")]
        [HttpGet]
        public HttpResponseMessage GetProblemOutputTestCases(int Id){
            try{
                MemoryStream mem_stream = DataRepository.GetProblemOutputTestCases(Id);
                
                return CreateTextFileResponse(mem_stream, "output.txt");

            }
            catch (ObjectNotFoundException e){
                var resposne = new HttpResponseMessage(HttpStatusCode.NotFound);
                return resposne;
            }
        }


        [HttpPost]
        [Route("create")]
        public async Task<IHttpActionResult> CreateProblem(){
            // request contain must be of type multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent()){
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try{
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);
            }
            catch (System.Exception e)
            {
                return InternalServerError(e);
            }

            var problem_form = new ProblemCreationForm(provider.FormData, provider.FileData);

            DataRepository.CreateProblem(problem_form);
            
            return Ok();
        }
      
        [Route("submit")]
        [HttpPost]
        public IHttpActionResult Submit(SubmissionRequestData data){
            var judge = new JudgeService();

            try{
                judge.judge(data);
            }
            catch (Exception e){
                logger.Error("Failed to judge submission", e);
            }
            
            return Ok();
        }
    }


    [RoutePrefix("api/all-submissions")]
    public class SubmissionsController : ApiController{

        [Route("")]
        [HttpGet]
        public Object AllSubmissions(SubmissionRequestData data){
            var ctx = new OjDBContext();
            var subs = ctx.Submissions.Where(x => true);
            return SubmissionResponseData.MapTo(subs);
        }
    }
}
