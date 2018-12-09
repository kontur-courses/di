using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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

        private readonly Dictionary<FontsEnum, Font> fonts = new Dictionary<FontsEnum, Font>
        {
            [FontsEnum.Regular] = new Font(FontFamily.GenericMonospace, 16, FontStyle.Regular),
            [FontsEnum.Bold] = new Font(FontFamily.GenericMonospace, 16, FontStyle.Bold)
        };

        private readonly FluentCommandLineParser parser;
        private Brush brush;
        private Font font;
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
            var options = new TagCloudCreationOptions(new ImageCreatingOptions(brush, font, new Point(500, 500)));
            var success = TryRead(wordsFile, out var words);
            if (!success)
            {
                Console.Error.WriteLine("Can not read given file");
                return;
            }

            var image = Creator.CreateImage(words, options);
            //TODO: check path for validness
            image.Save(outputPath);
            Console.Out.WriteLine($"Here you go{Environment.NewLine}\tFile is saved successfully");
        }

        private void SetupParser()
        {
            parser.Setup<string>('f', "words")
                  .Callback(arg => wordsFile = arg)
                  .Required();

            parser.Setup<string>('o', "output")
                  .Callback(arg => outputPath = arg)
                  .SetDefault($".{Path.DirectorySeparatorChar}tag_cloud.png");
            parser.Setup<FontsEnum>("font")
                  .Callback(arg => font = fonts[arg])
                  .SetDefault(FontsEnum.Regular);

            parser.Setup<BrushesEnum>("brush")
                  .Callback(arg => brush = brushes[arg])
                  .SetDefault(BrushesEnum.Simple);
        }

        [Flags]
        private enum BrushesEnum
        {
            Simple,
            Tough
        }
    }
}
