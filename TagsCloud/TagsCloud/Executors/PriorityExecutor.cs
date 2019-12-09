using System;
using System.Linq;
using TagsCloudGenerator.Interfaces;

namespace TagsCloudGenerator.Executors
{
    internal class PriorityExecutor<T> : IExecutor<T, T>
    {
        private readonly IPriorityExecutable<T, T>[] executables;

        public PriorityExecutor(IPriorityExecutable<T, T>[] executables)
        {
            if (executables == null)
                throw new ArgumentNullException();
            this.executables = executables
                .OrderByDescending(e => e.Priority)
                .ToArray();
        }

        public T Execute(T input)
        {
            var result = input;
            foreach (var executable in executables)
                result = executable.Execute(result);
            return result;
        }
    }
}