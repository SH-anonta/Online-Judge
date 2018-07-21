using System;
using System.Diagnostics;

namespace CodeRunner.Compilers {

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

    public interface Compiler{
        event EventHandler<CompilationFinishedEventArgs> OnCompilationFinished;
        void CompileSource(string source_code);
    } 
}
