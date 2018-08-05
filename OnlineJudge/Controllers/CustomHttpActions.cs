using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace OnlineJudge.Controllers {
    public class BadHttpRequest: IHttpActionResult {
        private IEnumerable<string> error_messages;

        public BadHttpRequest(IEnumerable<string> message){
            this.error_messages = message;
        }

        private string GetErrorMessagesAsJson(){
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return serializer.Serialize(error_messages);
        }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken){
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            response.Content = new StringContent(GetErrorMessagesAsJson());
            return Task.FromResult(response);
        }
    }
}