using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using CommandLine;
using TagsCloudContainer.ImageCreator;
using TagsCloudContainer.Visualizer;

namespace TagsCloudContainer.UI
{
    public class BasicConsoleUI : IUserInterface
    {
        public class Options
        {
            [Option('i', "input", Required = true, HelpText = "Path of input file with words")]
            public string Input { get; set; }

            [Option('o', "output", Required = true, HelpText = "Path of output image file")]
            public string Output { get; set; }

            [Option('w', "width", Required = false, HelpText = "Image width")]
            public int Width { get; set; }

            [Option('h', "height", Required = false, HelpText = "Image height")]
            public int Height { get; set; }
        }

        private readonly IImageCreator imageCreator;

        public BasicConsoleUI(IImageCreator imageCreator)
        {
            this.imageCreator = imageCreator;
        }

        public IInitialSettings GetSettings(IEnumerable<string> args)
        {
            InitialSettings settings = null;
            Parser.Default.ParseArguments<Options>(args).WithParsed(o =>
            {
                if (!File.Exists(o.Input))
                {
                    Console.WriteLine("Input file not exists");
                    return;
                }

                var inputPath = new FileInfo(o.Input).FullName;
                var outputPath = new FileInfo(o.Output).FullName;
                var imageSize = new Size(0, 0);
                if (o.Width > 0 && o.Height > 0)
                    imageSize = new Size(o.Width, o.Height);
                settings = new InitialSettings(inputPath, outputPath, imageSize);
            });
            if (settings == null)
                throw new Exception("Cant get settings with these arguments");
            return settings;
        }

        public void Run(IEnumerable<string> args)
        {
            var settings = GetSettings(args);
            imageCreator.CreateImage(settings);
        }
    }
}
