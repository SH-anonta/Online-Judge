using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeRunner.Compilers;
using JudgeCodeRunner.CompilerServices;

namespace JudgeCodeRunner {
    public enum ProgrammingLanguageEnum{
        Cpp89,
        Cpp11,
        C,
        Python3
    }

    public class CompilerFactory {
        private static string GPP_COMPILER_PATH= @"C:\Program Files (x86)\CodeBlocks\MinGW\bin\g++.exe";
        private static string GCC_COMPILER_PATH= @"C:\Program Files (x86)\CodeBlocks\MinGW\bin\gcc.exe";
        public static string PYTHON_INTERPRETER_PATH= @"C:\Program Files\Python36\python.exe";

//        private static string GPP_COMPILER_PATH= Environment.GetEnvironmentVariable("JUDGE_CPP_COMPILER");
//        private static string GCC_COMPILER_PATH= Environment.GetEnvironmentVariable("JUDGE_C_COMPILER");
//        public static string PYTHON_INTERPRETER_PATH= Environment.GetEnvironmentVariable("JUDGE_PYTHON36_COMPILER");
        
        public static Compiler getCompiler(ProgrammingLanguageEnum languageEnum){
            Compiler comp= null;

            if (languageEnum == ProgrammingLanguageEnum.Cpp89){
                comp = new CPPCompiler(GPP_COMPILER_PATH, "-std=c++98");
            }
            else if(languageEnum == ProgrammingLanguageEnum.Cpp11){
                comp = new CPPCompiler(GPP_COMPILER_PATH, "-std=c++11");
            }
            else if(languageEnum == ProgrammingLanguageEnum.C){
                comp = new CPPCompiler(GCC_COMPILER_PATH);
            }
            else if(languageEnum == ProgrammingLanguageEnum.Python3){
                comp = new PythonCompiler(PYTHON_INTERPRETER_PATH);
            }
            else{
                comp = null;
                throw new Exception("Invalid programming languageEnum enum: "+ languageEnum.ToString());
            }

            return comp;
        }
    }
}
