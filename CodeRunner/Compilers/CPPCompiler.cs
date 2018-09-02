using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeRunner.Compilers;

// todo turn deligate into events

namespace JudgeCodeRunner.CompilerServices {
    


    public class  CPPCompiler: Compiler{
        public event EventHandler<CompilationFinishedEventArgs> OnCompilationFinished;

        private string compiler_path;
        private string command_arguments;
        private string output_exe_path;


        public CPPCompiler(string compiler_path, string command_arguments= "")        {
            this.compiler_path = compiler_path;
            this.command_arguments = command_arguments;
        }
        
        // compiles given source code and creates exe file in output_file_path, if output file path is null,
        // exe file is created in temp directory
        public void CompileSource(string source_code){
            output_exe_path = Utility.GetTempFilePath();

            string file_path = Utility.GetTempFilePath()+".cpp";
            File.WriteAllText(file_path, source_code);

            Process compiler_proc = CreateCompilerProcessObject(file_path);
            compiler_proc.Exited += new EventHandler(this.compilationFinishedHandler);
            
            compiler_proc.Start();
        }

        private Process CreateCompilerProcessObject(string source_file_path){
            Process compiler_proc = new Process();

            compiler_proc.StartInfo.FileName = compiler_path;
            compiler_proc.StartInfo.Arguments = String.Format("{0} {1} -o {2}", this.command_arguments, source_file_path, output_exe_path);
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
