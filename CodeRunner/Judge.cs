using System;
using System.Diagnostics;
using System.Text;
using System.Timers;

namespace JudgeCodeRunner {
    public class ExecutionResult{
        public Verdict Verdict { get; }

        // contains either runtime error message or compilation time error message or nothing
        public string ErrorMsg { get; }
        public double RunningTime { get; }
        public double MemmoryUsage { get; }        

        public ExecutionResult(Verdict verdict, double running_time= 0, double memory_usage= 0, string error_msg=""){
            this.Verdict = verdict;
            this.ErrorMsg = error_msg;
            this.MemmoryUsage = memory_usage;
            this.RunningTime = running_time;
        }

        public override string ToString(){
            return String.Format("Verdict: {0}\nTime: {1}\nMemory: {2}\nErrorMsg: {3}",
                Verdict,MemmoryUsage,RunningTime,ErrorMsg);
        }
    }

    public class Judge {
        public event EventHandler<ExecutionResultEventArgs> OnExecutionFinished;
        private Process proc;
        private string input;
        private string expected_output;
        private double time_limit;

        private StringBuilder actual_output= new StringBuilder();

        private Stopwatch running_time_recorder = new Stopwatch();

        // flags
        private bool process_killed_by_TLE_timer= false;

        public Judge(Process proc, string expected_output, string input, double time_limit){
            this.proc = proc;
            this.expected_output = expected_output;
            this.input = input;
            this.time_limit = time_limit;
            
            // expects StartInfo.FileName of proc to be set
            // todo throw exception if path of exe is not set
            if (proc.StartInfo.FileName == null){
                throw new Exception("StartInfo.FileName of process must be set (by the compiler)");
            }

            
            proc.StartInfo.RedirectStandardInput= true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError= true;
            
            
            proc.EnableRaisingEvents = true;
            proc.StartInfo.UseShellExecute = false;
            
            proc.Exited += new EventHandler(this.onProgramExecutionFinish);
            
            proc.OutputDataReceived += (sender, e) => { actual_output.AppendLine(e.Data); };
        }

        public void Run(){           
            running_time_recorder.Start();
            proc.Start();

            // Provide the program with the test case input
            // this has to be called after the process is started
            proc.StandardInput.WriteLineAsync(input);

            proc.BeginOutputReadLine();

            // set timer to check wather the process exceeds time limit
            // time_limit is in seconds, needs to be converted to miliseconds
            Timer timer = new Timer(1000*time_limit);
            timer.Enabled = true;
            timer.Elapsed += new ElapsedEventHandler(TimeLimitCheckTimer);

//            proc.PriorityClass = ProcessPriorityClass.High;
//            proc.MaxWorkingSet = new IntPtr(1000);
//            Console.WriteLine(proc.PeakWorkingSet64);            
            // todo record peak memory usage
//            long peak_memory_usage = proc.PeakWorkingSet64;
        }

        // Event handlers
        private void onProgramExecutionFinish(object sender, EventArgs e){
            if (process_killed_by_TLE_timer){
                // the TLE verdict has already been submitted
                return;
            }

            running_time_recorder.Stop();

            double running_time = running_time_recorder.Elapsed.TotalSeconds;

            string output = actual_output.ToString();

            if (proc.ExitCode == 0){
//                Console.WriteLine("Program ran successfully");
//                Console.WriteLine("Program output");
//                Console.WriteLine("Actual----------------------------------");
//                Console.WriteLine(output);
//                Console.WriteLine("Expected----------------------------------");
//                Console.WriteLine(expected_output);
//                Console.WriteLine("------------------------------------------");

                // todo check output here
                Verdict verdict = output.TrimEnd() == expected_output.TrimEnd()? Verdict.Accepted : Verdict.WrongAnswer;
                ExecutionResult res = new ExecutionResult(verdict, running_time);
                OnExecutionFinished(this, new ExecutionResultEventArgs(res));
            }
            else{
                string error = proc.StandardError.ReadToEnd();
                ExecutionResult res = new ExecutionResult(Verdict.RunTimeError, running_time,0, error);
                OnExecutionFinished(this, new ExecutionResultEventArgs(res));
            }
        }

        private void TimeLimitCheckTimer(object sender, ElapsedEventArgs e){
            // if the program is still running
            if (!proc.HasExited){
                process_killed_by_TLE_timer = true;

                // time limit exceeded
                ExecutionResult exe_result = new ExecutionResult(Verdict.TimeLimitExceeded);
                OnExecutionFinished(this, new ExecutionResultEventArgs(exe_result));

                proc.Kill();
            }

            // this timer needs to run only once
            ((Timer)sender).Enabled = false;
            ((Timer)sender).Stop();
            ((Timer)sender).Dispose();
        }
    }

    // utility functions

}
