using System;
using System.CodeDom.Compiler;
using CodeRunner.Compilers;
using JudgeCodeRunner.CompilerServices;

namespace JudgeCodeRunner {
    class Driver
    {
        private static string RiskyCode = @"
import os

#os.listdir('G:/tmp')
f = open('G:/tmp/take.txt', 'w')
f.write('abc')
f.close()
        
";
        public static void Main(){
            testCodeRunner();
        }

        static void testCodeRunner(){
//            string source_path = DummyCode.getCPP89WACode();
//            string source_path = DummyCode.getCpp89Code();
            string source_path = DummyCode.getPython3Code();
//            ProgrammingLanguageEnum lang = ProgrammingLanguageEnum.Cpp89;
            ProgrammingLanguageEnum lang = ProgrammingLanguageEnum.Python3;

            double time_limit= 1;
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
