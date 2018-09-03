using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using CodeRunner.Compilers;
using JudgeCodeRunner.CompilerServices;
using Newtonsoft.Json;

namespace JudgeCodeRunner {
    

    public enum ProgrammingLanguageEnum{
        Cpp89,
        Cpp11,
        C,
        Python3
    }

    public class CompilerFactory{
        // reference for the singleton
        private static CompilerFactory single_instance;

        private static readonly string ENVIRONMENT_VARIABLE_NAME = "ONLINE_JUDGE_COMPILER_PATHS";

        private static string GPP_COMPILER_PATH;
//        private static string GCC_COMPILER_PATH= @"C:\Program Files (x86)\CodeBlocks\MinGW\bin\gcc.exe";
        private static string GCC_COMPILER_PATH;
        public static string PYTHON_INTERPRETER_PATH;


        private void loadCompilerPaths(){
            // a json object with values is expected
            string compiler_paths = Environment.GetEnvironmentVariable(ENVIRONMENT_VARIABLE_NAME, EnvironmentVariableTarget.Machine);

            if (compiler_paths == ""){
                throw new Exception("Environment variable ONLINE_JUDGE_COMPILER_PATHS not found");
            }
            
            var paths = JsonConvert.DeserializeObject<Dictionary<string, string>>(compiler_paths);

            GPP_COMPILER_PATH = paths["CPP"];
            GCC_COMPILER_PATH = paths["C"];
            PYTHON_INTERPRETER_PATH = paths["PYTHON36"];
        }

        private CompilerFactory(){
            loadCompilerPaths();
        }

        public static CompilerFactory getInstance(){
            if (single_instance == null){
                single_instance = new CompilerFactory();
            }

            return single_instance;
        }

        public Compiler getCompiler(ProgrammingLanguageEnum languageEnum){
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
                throw new Exception("Invalid programming languageEnum enum: "+ languageEnum);
            }

            return comp;
        }
    }
}
