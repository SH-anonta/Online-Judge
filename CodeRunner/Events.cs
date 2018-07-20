using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgeCodeRunner {
    
    public class ExecutionResultEventArgs : EventArgs{
        public ExecutionResult ExecutionResult { get; }

        public ExecutionResultEventArgs(ExecutionResult execution_result){
            this.ExecutionResult = execution_result;
        }
    }

}
