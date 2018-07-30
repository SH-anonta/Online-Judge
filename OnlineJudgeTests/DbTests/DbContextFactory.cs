using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineJudge.Models;

namespace OnlineJudgeTests.DbTests {
    class DbContextFactory {
        public static OjDBContext getDbContext(){
            return new OjDBContext(Effort.DbConnectionFactory.CreateTransient());
        }
    }
}
