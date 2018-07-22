using System;
using System.CodeDom.Compiler;
using CodeRunner.Compilers;
using JudgeCodeRunner.CompilerServices;

namespace JudgeCodeRunner {
    class Driver{      
        public static void Main(){
            testCodeRunner();
//            testCompiler();
        }

        static void testCodeRunner(){
//            string source_path = DummyCode.getCCode();
            string source_path = DummyCode.getPython3Code();
//            ProgrammingLanguageEnum lang = ProgrammingLanguageEnum.C;
            ProgrammingLanguageEnum lang = ProgrammingLanguageEnum.Python3;

            double time_limit= 0.5;
            CodeRunner runner = new CodeRunner(lang, source_path, DummyData.intput, DummyData.expected_output, time_limit);
            runner.OnExecutionFinished += CodeResultsHandler;
            runner.RunCode();

            // intentionally wait forever
            while (true){}
        }

        private static void CodeResultsHandler(object sender, ExecutionResultEventArgs args){
            Console.WriteLine(args.ExecutionResult);
        }
    }
}
