using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgeCodeRunner{
    public class Utility{
        // returns file path in temp directory which is very unlikely to be occupied
        public static string GetTempFilePath(){
            return System.IO.Path.GetTempPath() + Guid.NewGuid().ToString();
        }
    }
}