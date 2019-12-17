using System.Drawing;
using Autofac;
using CloudDrawing;
using CloudLayouter;
using CloudLayouter.Spiral;
using CommandLine;
using TagsCloudContainer.PreprocessingWords;
using TagsCloudContainer.ProcessingWords;
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
            builder.RegisterType<MyStemUtility>().As<IPreprocessingWords>();
            builder.RegisterType<Processor>().As<IProcessor>();
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(o =>
                {
                    var proc = builder.Build().Resolve<IProcessor>();
                    proc.Run(o.PathToFile,
                        o.PathSaveFile,
                        Color.Aquamarine,
                        "Arial",
                        Brushes.Black,
                        new StringFormat()
                        {
                            LineAlignment = StringAlignment.Center
                        },
                        new Size(1500, 1500));
                });
            
        }
    }
}