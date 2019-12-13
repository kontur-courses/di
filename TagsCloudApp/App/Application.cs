using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using CommandLine;
using TagsCloudApp.ImageSave;
using TagsCloudApp.LayOuter;
using TagsCloudApp.Reader;
using TagsCloudApp.ToSizeConverter;
using TagsCloudApp.Visualization;
using TagsCloudApp.WordFiltering;

namespace TagsCloudApp.App
{
    public class Application
    {
        public class Options
        {
            [Option('i', "input", Required = true, HelpText = "Input file with words")]
            public string Input{ get; set; }

            [Option('o', "output", Required = true, HelpText = "Output file path")]
            public string Output{ get; set; }

            [Option('w', "width", HelpText = "Width", Default = 2000)]
            public int Width { get; set; }

            [Option('h', "height", HelpText = "Height", Default = 2000)]
            public int Height { get; set; }

            [Option('f', "font", HelpText = "Font", Default = "Times New Roman")]
            public string FontFamily { get; set; }

            [Option('c', "colors", HelpText = "Word colors",Default ="Green")]
            public string WordsColors { get; set; }
        }

        private readonly IFileReader reader;
        private readonly IToSizeConverter wordConverter;
        private readonly IWordFilter filter;
        private readonly ICloudLayouter layouter;        
        private readonly IVisualisator visualisator;
        private readonly IImageSaver saver;

        
        public Application(IFileReader reader, IToSizeConverter wordConverter,
            IWordFilter filter, ICloudLayouter layouter, 
            IVisualisator visualisator, IImageSaver saver)
        {
            this.reader = reader;
            this.wordConverter = wordConverter;
            this.filter = filter;
            this.layouter = layouter;
            this.saver = saver;          
            this.visualisator = visualisator;
            this.saver = saver;
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
                var font = new Font(o.FontFamily, 14.0f);
                var color = Color.FromName(o.WordsColors);               
                imageSize = new Size(o.Width, o.Height);
                settings = new InitialSettings(inputPath, outputPath, imageSize, color, font);
            });
            if (settings == null)
            {
                throw new Exception("Cant get settings with these arguments");
            }
            return settings;
        }

        public void CreateImage(IEnumerable<string> args)
        {
            var settings = GetSettings(args);
            var reader = new FileTextReader();
            var words = reader.ReadWords(settings.InputFilePath);
            var filter = new Filter();
            var filteredWords = filter.FilterWords(words);
            var converter = new WordToSizeConverter();
            var wordsSizes = converter.ConvertToSizes(filteredWords);
            var visualisator = new Visualisator();            
            var color = settings.WordsColor;
            var font = settings.WordsFont;
            var layouter = new CircularCloudLayouter();
            var wordsRectangles = wordsSizes.Select(s => new Tuple<string, Rectangle>(s.Item1, layouter.PutNextRectangle(s.Item2)));
            var currentBitmap = visualisator.Visualize(wordsRectangles, settings.ImageSize, color, font);
            var saver = new Saver();
            saver.SaveImage(currentBitmap, settings.OutputFilePath);
        }
    }
}
