using System;
using Autofac;
using DocoptNet;
using TagsCloudContainer.TagCloudVisualization;
using TagsCloudContainer.WordProcessing;

namespace TagsCloudContainer
{
    public static class Program
    {
        private const string usage = @"Tag Cloud.
        Usage:
          tag_cloud.exe [<inputFile>] [--format=<format>] [--font=<font>] [--bgcolor=<bgcolor>]
 [--textcolor=<textcolor>]
          tag_cloud.exe (-d | --debug) [<inputFile>] [--format=<format>] [--font=<font>] [--bgcolor=<bgcolor>]
 [--textcolor=<textcolor>] [--dbgrectcolor=<dbgrectcolor>] [--dbgmarkcolor=<dbgmarkcolor>]
          tag_cloud.exe (-h | --help)

        Options:
          -h --help                        Show this screen.
          --format=<format>                Set image format [default: png]
          --font=<font>                    Set font [default: Arial]
          --bgcolor=<bgcolor>              Set background color [default: white]
          --textcolor=<textcolor>          Set text color [default: black]
          -d --debug                       Enable debug mode
          --dbgrectcolor=<dbgrectcolor>    Set word rectangle color [default: gray]
          --dbgmarkcolor=<dbgmarkcolor>    Set marking color [default: gray]
       ";
        
        public static void Main(string[] args)
        {
            var arguments = new Docopt().Apply(usage, args, exit: true);
            if (arguments["--help"].IsTrue)
            {
                Console.WriteLine(usage);
                return;
            }

            IContainer container;
            try
            {
                container = AutofacConfig.ConfigureContainer(arguments);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            RunProgramWithContainer(container);
        }

        private static void RunProgramWithContainer(IContainer container)
        {
            var wordProvider = container.Resolve<IWordProvider>();
            var wordNormalizer = container.Resolve<IWordNormalizer>();
            var wordFilter = container.Resolve<IWordFilter>();
            var wordCalculator = container.Resolve<IWordStatisticsCalculator>();
            var layouter = container.Resolve<ILayouter>();
            var visualizer = container.Resolve<IVisualizer>();
            var imgSaver = container.Resolve<IImageSaver>();

            var words = wordProvider.GetWords();
            words = wordNormalizer.NormalizeWords(words);
            words = wordFilter.Filter(words);
            var wordDatas = wordCalculator.CalculateStatistics(words);
            var tagCloudItems = layouter.PlaceWords(wordDatas);
            var bitmap = visualizer.Visualize(tagCloudItems);
            var savedImgPath = imgSaver.Save(bitmap);
            Console.WriteLine($"Tag cloud saved at {savedImgPath}");
        }
    }
}