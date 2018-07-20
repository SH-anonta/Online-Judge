using System;
using System.CodeDom.Compiler;
using JudgeCodeRunner.CompilerServices;

namespace JudgeCodeRunner {
    class Driver{      
        public static void Main(){
            testCodeRunner();
//            testCompiler();
        }

        static void testCodeRunner(){
            string source_path = CppCode.ac;


            double time_limit= 0.5;
            CodeRunner runner = new CodeRunner(source_path, DummyData.intput, DummyData.expected_output, time_limit);
            runner.OnExecutionFinished += CodeResultsHandler;
            runner.RunCode();

            // intentionally wait forever
            while (true){}
        }

        private static void CodeResultsHandler(object sender, ExecutionResultEventArgs args){
            Console.WriteLine(args.ExecutionResult);
        }

        static void testCompiler(){
            string source_path = @"G:\compile\ac.cpp";
            CPPCompiler cpp = new CPPCompiler();
//            cpp.CompileSource(handler, source_path, @"G:\compile\exes\auto.exe");
//            cpp.CompileSource(handler, source_path);


            cpp.OnCompilationFinished += handler;
            cpp.CompileSource(source_path);

//          intentionally wait forever
            while (true){}
        }

        static void handler(object sender, CompilationFinishedEventArgs args){
            Console.WriteLine(args.compilation_result);
        }
    }
}
