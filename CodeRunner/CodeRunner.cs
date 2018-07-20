using System;
using System.Diagnostics;
using JudgeCodeRunner.CompilerServices;

namespace JudgeCodeRunner{
    public enum Verdict{
        Accepted,
        WrongAnswer,
        RunTimeError,
        CompilationError,
        TimeLimitExceeded,
        MemoryLimitExceeded
    }

    public class CodeRunner{
        public event EventHandler<ExecutionResultEventArgs> OnExecutionFinished;

        private static string gcc_compiler_path = @"C:\Program Files (x86)\CodeBlocks\MinGW\bin\g++.exe";
        private static string output_exe_name = @"G:\compile\exes\code.exe";
        private CPPCompiler compiler = new CPPCompiler();
        
        
        private string input;               // input for the program being judged
        private string expected_output;
        private string source_file_path;
        private double time_limit;


        // pocess whtich executes the submitted source code
        private Process proc;

        // time_limit is in seconds
        public CodeRunner(string source_file_path, string input, string expected_output, double time_limit){
            this.input = input;
            this.expected_output = expected_output;
            this.source_file_path = source_file_path;
            this.time_limit = time_limit;
        }
        
        // used by client 
        public void RunCode(){
            compiler.OnCompilationFinished += compilationFinishedHandler;
            compiler.CompileSource(source_file_path, output_exe_name);
        }


        private void compilationFinishedHandler(object sender, CompilationFinishedEventArgs args){
            CompilationResult result = args.compilation_result;

            if (result.CompilationSuccessful){
                runCompiledProgram(result.process);
            }
            else{
                ExecutionResult exe_result = new ExecutionResult(Verdict.CompilationError, 0, 0, result.StandardError);
                OnExecutionFinished(this,new ExecutionResultEventArgs(exe_result));
            }
        
        }

        private void runCompiledProgram(Process process){
            Judge judge =  new Judge(process, expected_output, input, time_limit);

            judge.OnExecutionFinished += (sender, e) => {
                OnExecutionFinished(this, new ExecutionResultEventArgs(e.ExecutionResult));
            };

            judge.Run();
        }

    }

    
    
}
