using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Fclp;
using TagCloudCreation;
using TagCloudVisualization;
using Point = TagCloudVisualization.Point;

namespace TagCloudApp
{
    internal class ConsoleUserInterface : UserInterface
    {
        [Flags]
        public enum FontsEnum
        {
            Regular,
            Bold
        }

        private readonly Dictionary<BrushesEnum, Brush> brushes = new Dictionary<BrushesEnum, Brush>
        {
            [BrushesEnum.Simple] = Brushes.Chartreuse, [BrushesEnum.Tough] = SystemBrushes.ControlDarkDark
        };

        

        private readonly FluentCommandLineParser parser;
        private Brush brush;
        private Point center;
        private string font;
        private string outputPath;

        private string wordsFile;

        public ConsoleUserInterface(TagCloudCreator creator, IEnumerable<ITextReader> readers) : base(creator, readers)
        {
            parser = new FluentCommandLineParser();
            SetupParser();
        }

        public override void Run(string[] startupArgs)
        {
            parser.Parse(startupArgs);

            //TODO: add center option for user
            //TODO: add more options for user: font size, gradient brushes
            var options = new TagCloudCreationOptions(new ImageCreatingOptions(brush, font, center));
            if (!TryRead(wordsFile, out var words))
            {
                Console.Error.WriteLine("Can not read given file");
                return;
            }

            var image = Creator.CreateImage(words, options);
            //TODO: check path for validness
            image.Save(outputPath);
            image.Dispose();
            Console.Out.WriteLine($"Here you go{Environment.NewLine}\tFile is saved successfully");
        }

        private void SetupParser()
        {
            parser.Setup<string>('f', "words")
                  .Callback(arg => wordsFile = arg)
                  .Required();

            parser.Setup<string>('c', "center")
                  .WithDescription("two integers separated by space: x y")
                  .Callback(SetCenter)
                  .SetDefault("500 500");

            parser.Setup<string>('o', "output")
                  .Callback(arg => outputPath = arg)
                  .SetDefault($"..{Path.DirectorySeparatorChar}tag_cloud.png");

            parser.Setup<string>("font")
                  .Callback(arg => font = arg)
                  .SetDefault("Microsoft Sans Serif");

            parser.Setup<BrushesEnum>("brush")
                  .Callback(arg => brush = brushes[arg])
                  .SetDefault(BrushesEnum.Simple);
        }


        private void SetCenter(string rawCenter)
        {
            var coordinates = rawCenter.Split(null)
                                       .Select(int.Parse).ToArray();
            center = new Point(coordinates[0],coordinates[1]);
        }

        [Flags]
        private enum BrushesEnum
        {
            Simple,
            Tough
        }
    }
}
