using System.Drawing;
using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using TagsCloudVisualization.Curves;
using TagsCloudVisualization.FileReaders;
using TagsCloudVisualization.TextFormatters;

namespace TagsCloudVisualization
{
    internal class Program
    {
        public static ServiceProvider Container;

        private static void Main(string[] args)
        {
            var parsedArgs = Parser.Default.ParseArguments<CommandLineOptions>(args).Value;
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<IFileReader>(new DocFileReader(parsedArgs.InputFile));
            services.AddSingleton<IFileReader>(new TxtFileReader(parsedArgs.InputFile));
            services.AddSingleton(new Spiral(new Point(parsedArgs.X, parsedArgs.Y)));
            services.AddSingleton<IPainter>(new Painter(new Size(parsedArgs.Width, parsedArgs.Height), parsedArgs.Colors));
            services.AddSingleton<ICloudLayouter>(new CircularCloudLayouter(new Point(parsedArgs.X, parsedArgs.Y)));
            services.AddSingleton<IWordFilter>(new SmallWordFilter());
            services.AddSingleton<ITextFormatter, TextFormatter>();
            services.AddSingleton<Client>();
            Container = services.BuildServiceProvider();
            var font = new FontFamily(parsedArgs.FontName);
            var client = Container.GetRequiredService<Client>();

            client.Draw(parsedArgs.OutputFile, font);
        }
    }
}