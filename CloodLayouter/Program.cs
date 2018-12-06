using System;
using System.Linq;
using CommandLine;
using Autofac;
using CloodLayouter.App;
using CloodLayouter.Infrastructer;
using CloudLayouter.App;
using CloudLayouter.Infrastructer;

namespace CloodLayouter
{
    internal class Program
    {
      
        public static void Main(string[] args)
        {
            var logicBuilder = new ContainerBuilder();
            logicBuilder.RegisterType<SimpleWordSelector>().As<IWordSelector>();
            logicBuilder.RegisterType<FileStreamReader>().As<IStreamReader>();
            logicBuilder.RegisterType<SimpleConverter>().As<IConverter>();
            logicBuilder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
            logicBuilder.RegisterType<ImageDirectoryProvider>().As<IImageDirectoryProvider>();
            logicBuilder.RegisterType<GraphicsHolder>().As<IGraphicsHolder>();
            logicBuilder.RegisterType<LogicPerformer>();
            
            var logicContainer = logicBuilder.Build();

            var logicPerformer = logicContainer.Resolve<LogicPerformer>();
            logicPerformer.Perfom(args[0]);
        }
    }
}