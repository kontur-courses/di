using System.Drawing;
using System.IO;
using System.Linq;
using CommandLine;
using Autofac;

namespace TagCloud.Visualizer.Console
{
    internal static class CloudVisualizer
    {
        private static void Main(string[] args)
        {
            var options = new Options();
            var cloudLayouterService = new ContainerBuilder();
            cloudLayouterService.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>().WithParameter(
                new TypedParameter(typeof(Point), new Point(options.ImageWidth / 2, options.ImageHeight / 2)));
            var words = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(),
                "..",
                "..",
                "..",
                "..",
                "TagCloud.Visualizer",
                "source",
                "input.txt"))
                .Select(word => word.ToLower());
            do
            {
                args = System.Console.ReadLine()?.Split(' ');
                if (args?[0] == "print")
                {
                    var cloudLayouterBuilder = cloudLayouterService.Build();
                    var cloudLayouter = cloudLayouterBuilder.Resolve<ICloudLayouter>(); 
                    //Почему-то создаётся объект с центром в точке (0; 0), а не в той, что я указал в RegisterType<>
                }
                else if (Parser.Default.ParseArguments<Options>(args) is Parsed<Options> parsedOptions)
                {
                    options = parsedOptions.Value;
                    cloudLayouterService.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>().WithParameter(
                        new TypedParameter(typeof(Point), new Point(options.ImageWidth / 2, options.ImageHeight / 2)));
                }
                else
                {
                    System.Console.WriteLine("Некорректная команда");
                }
            } while (args?[0] != "exit");
        }
    }
}