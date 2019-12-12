using System;
using Autofac;
using CommandLine;
using TagsCloudContainer.CloudDrawing;
using TagsCloudContainer.CloudLayouter;
using TagsCloudContainer.CloudLayouter.Spiral;
using TagsCloudContainer.PreprocessingWorld;
using TagsCloudContainer.ProcessingWorld;
using TagsCloudContainer.Reader;

namespace TagsCloudContainer
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            
            builder.RegisterType<CircularSpiral>().As<ISpiral>();
            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
            builder.RegisterType<CircularCloudDrawing>().As<ICircularCloudDrawing>();
            builder.RegisterType<ReaderFromTxt>().As<IReader>();
            builder.RegisterType<MyStemUtility>().As<IPreprocessingWorld>();
            builder.RegisterType<Processor>().As<IProcessor>();
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(o =>
                {
                    var proc = builder.Build().Resolve<IProcessor>();
                    proc.Run(o.PathToFile, o.PathSaveFile);
                });
            
        }
    }
}