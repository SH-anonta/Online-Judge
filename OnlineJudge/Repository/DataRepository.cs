using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineJudge.Models;

namespace OnlineJudge.Repository {
    public partial class DataRepository {
        public OjDBContext context = new OjDBContext();
    }
}