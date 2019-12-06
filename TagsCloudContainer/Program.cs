using System;
using System.Drawing.Imaging;
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
          tag_cloud.exe <inputFile> [--font=<font>] [--format=<format>]
          tag_cloud.exe --help

        Options:
          --help     Show this screen.
          --font=<font> Set font
          --format=<format>

        ";
        
        public static void Main(string[] args)
        {
            var arguments = new Docopt().Apply(usage, args, exit: true);
            if (arguments["--help"].IsTrue)
            {
                Console.WriteLine(usage);
            }

            var font = arguments["--font"] == null ? "Arial" : arguments["--font"].ToString();
            var imgFormat = arguments["--format"] == null ? "bmp" : arguments["--format"].ToString();
            var inputFile = arguments["<inputFile>"].ToString();
            
            var container = AutofacConfig.ConfigureContainer(font);
            var wp = container.Resolve<WordProcessor>();
            var vis = container.Resolve<LayoutVisualization>();
            
            var words = wp.Process(inputFile);
            var bm = vis.Visualize(words);
            ImgSaver.Save(bm, imgFormat);
        }
    }
}