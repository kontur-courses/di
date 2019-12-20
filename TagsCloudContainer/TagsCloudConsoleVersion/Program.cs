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
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<CircularSpiral>().As<ISpiral>();
            builder.RegisterType<CircularCloudDrawing>().As<ICircularCloudDrawing>();
            builder.RegisterType<ReaderLinesFromTxt>().As<IReaderLinesFromFile>();
            builder.RegisterType<MyStemUtility>().As<IPreprocessingWords>();
            builder.RegisterType<CreateProcess>().As<ICreateProcess>();
            builder.RegisterType<Processor>().As<IProcessor>();

            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(option =>
                {
                    if (option.UseSqueezedAlgorithm)
                        builder.RegisterType<SqueezedCircularCloudLayouter>().As<ICloudLayouter>();
                    else
                        builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
                    var proc = builder.Build().Resolve<IProcessor>();

                    var imageSettings = new ImageSettings(Color.FromName(option.ColorBackground),
                        new Size(option.Width, option.Height));
                    var wordDrawSettings = new WordDrawSettings(option.FamyilyNameFont,
                        new SolidBrush(Color.FromName(option.ColorBrush)),
                        new StringFormat()
                        {
                            LineAlignment = StringAlignment.Center
                        },
                        option.HaveDelineation);
                    proc.Run(option.PathToFile, option.PathSaveFile, imageSettings, wordDrawSettings);
                });
        }
    }
}