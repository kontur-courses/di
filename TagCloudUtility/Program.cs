using System;
using Autofac;
using TagCloud.Utility.Container;
using TagCloud.Utility.Runner;

namespace TagCloud.Utility
{
    public static class TagCloudProgram
    {
        public static void Execute(Options options)
        {
            if (options == null)
                throw new ArgumentNullException($"{nameof(options)} was null");

            Helper.CheckPaths(options);

            var container = ContainerConfig.Configure(options);

            var runner = container.Resolve<ITagCloudRunner>();

            runner.Run();
        }
    }
}