using Autofac;
using CommandLine;

namespace TagsCloudContainer
{
    public class Options
    {
        [Option("file", HelpText = "Text file", Required = true )]
        public string Filename { get; set; }
        [Option("color", HelpText = "Words color", Default = "DarkGreen")]
        public string Color { get; set; }
        [Option("font", HelpText = "Font", Default = "Arial")]
        public string FontFamilyName { get; set; }
        [Option("height", HelpText = "Image height", Default = "1080")]
        public string Height { get; set; }
        [Option("width", HelpText = "Image width", Default = "1920")]
        public string Width { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed(o =>
            {
                var source = new AdvancedSource(o.Filename);
                var cloudOptions = new CloudOptions(o.Color, o.FontFamilyName, o.Height, o.Width);

                var builder = new ContainerBuilder();
                builder.RegisterType<WordsPreprocessor>().As<IWordsPreprocessor>();
                builder.RegisterType<SimpleCloudConfigurator>().As<ICloudConfigurator>();
                builder.RegisterType<CloudVisualizer>().As<ICloudVisualizer>();
                builder.RegisterInstance(source).As<ISource>();
                builder.RegisterInstance(cloudOptions).As<CloudOptions>();

                var container = builder.Build();
                var client = container.Resolve<ICloudVisualizer>();

                client.VisualizeCloud();
            });
        }
    }
}
