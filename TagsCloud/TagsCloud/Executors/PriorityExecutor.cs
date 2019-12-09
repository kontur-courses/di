using System.Linq;
using TagsCloudGenerator.Attributes;
using TagsCloudGenerator.Extensions;
using TagsCloudGenerator.Interfaces;

namespace TagsCloudGenerator.Executors
{
    internal class PriorityExecutor<T> : IExecutor<T, T>
    {
        private readonly IExecutable<T, T>[] executables;

        public PriorityExecutor(IExecutable<T, T>[] executables) => 
            this.executables = executables;

        public T Execute(T input)
        {
            var result = input;
            foreach (var executable in executables
                .OrderByDescending(e => e
                    .GetType()
                    .GetFirstAttributeObj<PriorityAttribute>()
                    .Priority))
                result = executable.Execute(result);
            return result;
        }
    }
}