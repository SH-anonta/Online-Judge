using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JudgeCodeRunner.CompilerServices;

namespace JudgeCodeRunner {
    public class CompilerFactory {

        public static CPPCompiler createCPPCompiler(){
            return new CPPCompiler();
        }
    }
}
