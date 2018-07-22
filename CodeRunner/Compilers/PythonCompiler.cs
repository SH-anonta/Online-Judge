﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JudgeCodeRunner;

namespace CodeRunner.Compilers {
    class PythonCompiler: Compiler {
        public event EventHandler<CompilationFinishedEventArgs> OnCompilationFinished;
        private static string PYTHON_INTERPRETER_PATH= @"C:\Program Files\Python36\python.exe";

        public PythonCompiler(){

        }

        public void CompileSource(string source_code){
            // todo check for syntax error before compiling

            string file_path = Utility.GetTempFilePath()+".py";
            File.WriteAllText(file_path, source_code);

            Process compiled = GenerateProcessOfCompiledCode(file_path);

            var result= new CompilationResult(true, 0, compiled, "");
            OnCompilationFinished(this, new CompilationFinishedEventArgs(result));
        }

        private Process GenerateProcessOfCompiledCode(string output_exe_path){
            Process process = new Process();

            process.StartInfo.FileName = PYTHON_INTERPRETER_PATH;
            process.StartInfo.Arguments = output_exe_path;
            return process;
        }
    }
}
