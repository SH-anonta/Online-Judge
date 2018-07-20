using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// todo turn deligate into events

namespace JudgeCodeRunner.CompilerServices {
    public class CompilationResult{
        public bool CompilationSuccessful { get; }
        public int ExitCode { get; }
        public string StandardError { get; }
        public Process process { get; }

        public CompilationResult(bool success, int exit_code, Process process, string standard_error=""){
            CompilationSuccessful = success;
            ExitCode = exit_code;
            StandardError = standard_error;
            this.process = process;
        }

        public override string ToString(){
            return String.Format("Success: {0}\nExitCode: {1}\n\nStd Error: {2}",
                                CompilationSuccessful,ExitCode, StandardError);
        }
    }

    public class CompilationFinishedEventArgs :EventArgs{
        public CompilationResult compilation_result { get; }
        

        public CompilationFinishedEventArgs(CompilationResult result){
            compilation_result = result;
        }
    }


    public class CPPCompiler{
        public event EventHandler<CompilationFinishedEventArgs> OnCompilationFinished;

        private static string gcc_compiler_path = @"C:\Program Files (x86)\CodeBlocks\MinGW\bin\g++.exe";

        private string output_exe_path;


        // compiles given source code and creates exe file in output_file_path, if output file path is null,
        // exe file is created in temp directory
        public string CompileSource(string source_path, string output_file_path = null){

            output_exe_path = output_file_path ?? Utility.GetTempFilePath();

            Process compiler_proc = CreateCompilerProcessObject(source_path);
            compiler_proc.Exited += new EventHandler(this.compilationFinishedHandler);
            
            compiler_proc.Start();

            return output_file_path;
        }

        private Process CreateCompilerProcessObject(string source_file_path){
            Process compiler_proc = new Process();

            compiler_proc.StartInfo.FileName = gcc_compiler_path;
            compiler_proc.StartInfo.Arguments = String.Format("{0} -o {1}", source_file_path, output_exe_path);
            compiler_proc.StartInfo.RedirectStandardOutput = true;
            compiler_proc.StartInfo.RedirectStandardError= true;

            compiler_proc.EnableRaisingEvents = true;
            compiler_proc.StartInfo.UseShellExecute = false;
            
            return compiler_proc;
        }

        private Process GenerateProcessOfCompiledCode(){
            Process process = new Process();

            process.StartInfo.FileName = this.output_exe_path;
            return process;
        }

        private void compilationFinishedHandler(object sender, EventArgs e){
            Process process = (Process) sender;
            int exit_code = process.ExitCode;
            bool success = process.ExitCode == 0;
            
            
            // Error message (from std error stream) is passed only if compilaion fails
            string std_error = "";
            
            
            // a process object is passed only if compilation was successful
            Process compiled_process = null;

            if (success){
                compiled_process = GenerateProcessOfCompiledCode();
            }
            else{
                std_error = process.StandardError.ReadToEnd();
            }

            CompilationResult result = new CompilationResult(success, exit_code, compiled_process, std_error);
            OnCompilationFinished(this,new CompilationFinishedEventArgs(result));
        }


    }
}
