using System;
using System.Linq;
using TagsCloudGenerator.Interfaces;

namespace TagsCloudGenerator.Executors
{
    internal class StrArrToStrArrPriorityExecutor : IExecutor<string[], string[]>
    {
        private readonly IPriorityExecutable<string[], string[]>[] executables;

        public StrArrToStrArrPriorityExecutor(IPriorityExecutable<string[], string[]>[] executables)
        {
            if (executables == null)
                throw new ArgumentNullException();
            this.executables = executables
                .OrderByDescending(e => e.Priority)
                .ToArray();
        }

        public string[] Execute(string[] input)
        {
            var result = input;
            foreach (var executable in executables)
                result = executable.Execute(result);
            return result;
        }
    }
}