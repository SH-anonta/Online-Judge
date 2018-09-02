using System;
using System.CodeDom.Compiler;
using CodeRunner.Compilers;
using JudgeCodeRunner.CompilerServices;

namespace JudgeCodeRunner {
    class Driver
    {
        static double time_limit= 1;
        private static string RiskyCode = @"
import os

#os.listdir('G:/tmp')
f = open('G:/tmp/take.txt', 'w')
f.write('abc')
f.close()
        
";

        private static string HighMemoryCode = @"
#include <iostream>

using namespace std;

int main(int argc, char* argv[]){
    double array[100000];

    auto x = 1100;
    int t, a,b;
    cin>>t;

    for(int i= 0; i<t; i++){
        cin>>a>>b;
        cout<< a+b<<endl;
    }


    return 0;
}

";
        public static void Main(){
            testCodeRunner();
        }

        static void testCodeRunner(){
            
//            TestCCompiler();
            TestCpp11Compiler();
//            TestCpp89Compiler();
//            TestPython3Compiler();

            // intentionally wait forever
            while (true){}
        }

        private static void CodeResultsHandler(object sender, ExecutionResultEventArgs args){
            Console.WriteLine(args.ExecutionResult);
        }

        private static void TestCpp11Compiler(){
            string source_path = DummyCode.getCpp11Code();
            ProgrammingLanguageEnum lang = ProgrammingLanguageEnum.Cpp11;

            CodeRunner runner = new CodeRunner(lang, source_path, DummyData.intput, DummyData.expected_output, time_limit);
            runner.OnExecutionFinished += CodeResultsHandler;
            runner.RunCode();
        }

        private static void TestCpp89Compiler(){
            string source_path = DummyCode.getCpp89Code();
            ProgrammingLanguageEnum lang = ProgrammingLanguageEnum.Cpp89;

            CodeRunner runner = new CodeRunner(lang, source_path, DummyData.intput, DummyData.expected_output, time_limit);
            runner.OnExecutionFinished += CodeResultsHandler;
            runner.RunCode();
        }

        private static void TestCCompiler(){
            string source_path = DummyCode.getCCode();
            ProgrammingLanguageEnum lang = ProgrammingLanguageEnum.C;

            CodeRunner runner = new CodeRunner(lang, source_path, DummyData.intput, DummyData.expected_output, time_limit);
            runner.OnExecutionFinished += CodeResultsHandler;
            runner.RunCode();
        }

        private static void TestPython3Compiler(){
            string source_path = DummyCode.getPython3Code();
            ProgrammingLanguageEnum lang = ProgrammingLanguageEnum.Python3;

            
            CodeRunner runner = new CodeRunner(lang, source_path, DummyData.intput, DummyData.expected_output, time_limit);
            runner.OnExecutionFinished += CodeResultsHandler;
            runner.RunCode();
        }
    }
}
