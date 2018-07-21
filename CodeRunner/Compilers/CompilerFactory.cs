using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeRunner.Compilers;
using JudgeCodeRunner.CompilerServices;

namespace JudgeCodeRunner {
    public enum ProgrammingLanguage{
        Cpp89,
        Cpp11,
        C,
        Python3
    }

    public class CompilerFactory {
        private static string GPP_COMPILER_PATH= @"C:\Program Files (x86)\CodeBlocks\MinGW\bin\g++.exe";
        private static string GCC_COMPILER_PATH= @"C:\Program Files (x86)\CodeBlocks\MinGW\bin\gcc.exe";

        public static Compiler getCompiler(ProgrammingLanguage language){
            Compiler comp= null;

            if (language == ProgrammingLanguage.Cpp89){
                comp = new CPPCompiler(GPP_COMPILER_PATH, "-std=c++98");
            }
            else if(language == ProgrammingLanguage.Cpp11){
                comp = new CPPCompiler(GPP_COMPILER_PATH, "-std=c++11");
            }
            else if(language == ProgrammingLanguage.C){
                comp = new CPPCompiler(GCC_COMPILER_PATH);
            }
            else if(language == ProgrammingLanguage.Python3){
                // todo implement
            }
            else{
                comp = null;
                throw new Exception("Invalid programming language enum: "+ language.ToString());
            }

            return comp;
        }
    }
}
