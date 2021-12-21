using System;

namespace TagsCloudVisualization.Processors
{
    public abstract class CommandProcessorBase<T>
    {
        public int Run(T options)
        {
            try
            {
                Process(options);
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 1;
            }
        }

        protected abstract void Process(T options);
    }
}