using System;
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
        private UserService user_service = new UserService();

        private static readonly log4net.ILog logger =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        public ProblemRepository problem_repository = new ProblemRepository();
        
        // return all problems in order of creation date
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetProblemList(){
            var problems = problem_repository.GetAllProblemsList();
            return Ok(ProblemListItem.MapTo(problems));
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult GetProblemList(int start, int limit){
            var problems = problem_repository.GetProblems(start, limit);
            
            
            return Ok(new CollectionResponse(){
                TotalCount = problem_repository.GetProblemCount(),
                Collection = ProblemListItem.MapTo(problems)
            });
        }

        [Route("{Id}")]
        [HttpGet]
        public IHttpActionResult GetProblemDetials(int Id){
            if (!user_service.IsAuthorizedToViewProblem(Id)){
                return Unauthorized();
            }

            try{
                ProblemDetails problem = problem_repository.GetProblemDetails(Id);
                return Ok(problem);
            }
            catch (ObjectNotFoundException e){
                return NotFound();
            }
        }

        private HttpResponseMessage CreateTextFileResponse(MemoryStream mem_stream, string file_name){
            var response = new HttpResponseMessage(HttpStatusCode.OK){
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
            if (!user_service.IsAuthorizedToAccessProblemTestCaseFiles(Id)){
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }

            try{
                MemoryStream mem_stream = problem_repository.GetProblemInputTestCases(Id);
                
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
            if (!user_service.IsAuthorizedToAccessProblemTestCaseFiles(Id)){
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }

            try{
                MemoryStream mem_stream = problem_repository.GetProblemOutputTestCases(Id);
                
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
            if (!user_service.IsAuthorizedToCreateProblem()){
                return Unauthorized();
            }

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
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            var problem_form = new ProblemCreationForm(provider.FormData, provider.FileData);

            FormDataValidationResult result = problem_form.Validate();

            if (result.IsValid){
                problem_repository.CreateProblem(problem_form);
                return Ok();
            }
            else{
                return new BadHttpRequest(result.ErrorMessages);
            }
        }

        [HttpPost]
        [Route("{id}/edit")]
        public async Task<IHttpActionResult> UpdateProblem(int id){
            if (!user_service.IsAuthorizedToEditProblem(id)){
                return Unauthorized();
            }

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
            catch (Exception e){
                return InternalServerError(e);
            }

            var problem_form = new ProblemCreationForm(provider.FormData, provider.FileData);

            FormDataValidationResult result = problem_form.Validate();
            if(!result.IsValid){
                return new BadHttpRequest(result.ErrorMessages);
            }

            try{
                problem_repository.UpdateProblem(id, problem_form);
                }
                catch (ObjectNotFoundException e){
                    return InternalServerError(e);
                }
                return Ok();
            }


        [HttpPost]
        [Route("{id}/delete")]
        public IHttpActionResult DeleteProblem(int id){
            if (!user_service.IsAuthorizedToDeleteProblem(id)){
                return Unauthorized();
            }

            try
            {
                problem_repository.DeleteProblem(id);
            }
            catch (ObjectNotFoundException e){
                return InternalServerError(e);
            }
            
            return Ok();
        }
        
        

    }

    [RoutePrefix("api/problems")]
    public class ProblemSubmissionController : ApiController{
        private UserService user_service = new UserService();
        private SubmissionRepository submission_repository;

        ProblemSubmissionController(){
            submission_repository = new SubmissionRepository();
        }

        [HttpPost]
        [HttpOptions]
        [Route("{problem_id}/submit")]
        public IHttpActionResult Submit(int problem_id, [FromBody]SubmissionFormData data){
            if (!user_service.IsAuthorizedToSubmitToProblem(problem_id)){
                return Unauthorized();
            }

            if (RequestUtility.IsPreFlightRequest(Request)){
                return Ok();
            }


            try{
                submission_repository.CreateProblemSubmission(problem_id, data);
                return Ok();
            }
            catch (ObjectNotFoundException e){
                return NotFound();
            }
        }
        
    }

    [RoutePrefix("api/all-submissions")]
    public class SubmissionsController : ApiController{

        [Route("")]
        [HttpGet]
        public Object AllSubmissions(SubmissionFormData data){
            var ctx = new OjDBContext();
            var subs = ctx.Submissions.Where(x => true);
            return SubmissionResponseData.MapTo(subs);
        }
    }


}
