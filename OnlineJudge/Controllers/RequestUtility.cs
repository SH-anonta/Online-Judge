using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace OnlineJudge.Controllers {
    public class RequestUtility {
        public static bool IsPreFlightRequest(HttpRequestMessage request){
            return request.Method == HttpMethod.Options;
        }
    }
}