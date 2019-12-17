using System.Drawing;
using Autofac;
using CloudDrawing;
using CloudLayouter;
using CloudLayouter.Spiral;
using CommandLine;
using TagsCloudContainer.PreprocessingWords;
using TagsCloudContainer.ProcessingWords;
using TagsCloudContainer.Reader;

namespace TagsCloudConsoleVersion
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<CircularSpiral>().As<ISpiral>();
            
            builder.RegisterType<CircularCloudDrawing>().As<ICircularCloudDrawing>();
            builder.RegisterType<ReaderFromTxt>().As<IReader>();
            builder.RegisterType<MyStemUtility>().As<IPreprocessingWords>();
            builder.RegisterType<Processor>().As<IProcessor>();
            
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(o =>
                {
                    if (o.UseSqueezedAlgorithm)
                        builder.RegisterType<SqueezedCircularCloudLayouter>().As<ICloudLayouter>();
                    else
                        builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
                    var proc = builder.Build().Resolve<IProcessor>();
                    
                    var bitmap = proc.Run(o.PathToFile,
                        Color.FromName(o.ColorBackground), 
                        o.FamyilyNameFont,
                        new SolidBrush(Color.FromName(o.ColorBrush)),
                        new StringFormat()
                        {
                            LineAlignment = StringAlignment.Center
                        },
                        new Size(o.Width, o.Height));
                    bitmap.Save(o.PathSaveFile);
                });
        }
    }
}