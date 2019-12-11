using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using CommandLine;
using TagsCloudContainer.ImageSaver;
using TagsCloudContainer.ImageSizeCalculator;
using TagsCloudContainer.Layouter;
using TagsCloudContainer.Reader;
using TagsCloudContainer.RectanglesTransformer;
using TagsCloudContainer.UI;
using TagsCloudContainer.Visualizer;
using TagsCloudContainer.WordProcessor;
using TagsCloudContainer.WordsToSizesConverter;

namespace TagsCloudContainer.Ui
{
    public class ConsoleTagsCloudCreator : IUi
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

        private readonly ITextReader reader;
        private readonly IWordProcessor wordProcessor;
        private readonly IWordFrequenciesToSizesConverter converter;
        private readonly ILayouter layouter;
        private readonly IImageSizeCalculator imageSizeCalculator;
        private readonly IRectanglesTransformer rectanglesTransformer;
        private readonly IVisualizer visualizer;
        private readonly IImageSaver saver;

        public ConsoleTagsCloudCreator(ITextReader reader, IWordProcessor wordProcessor, 
            IWordFrequenciesToSizesConverter converter, ILayouter layouter, IImageSizeCalculator imageSizeCalculator,
            IRectanglesTransformer rectanglesTransformer, IVisualizer visualizer, IImageSaver saver)
        {
            this.reader = reader;
            this.wordProcessor = wordProcessor;
            this.converter = converter;
            this.layouter = layouter;
            this.imageSizeCalculator = imageSizeCalculator;
            this.rectanglesTransformer = rectanglesTransformer;
            this.visualizer = visualizer;
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
                if (o.Width > 0 && o.Height > 0)
                    imageSize = new Size(o.Width, o.Height);
                settings = new InitialSettings(inputPath, outputPath, imageSize);
            });
            if (settings == null)
                throw new Exception("Cant get settings with these arguments");
            return settings;
        }

        public void CreateImage(IEnumerable<string> args)
        {
            var settings = GetSettings(args);
            var words = reader.ReadWords(settings.InputFilePath);
            var wordsWithCount = wordProcessor.ProcessWords(words).ToList();
            var sizes = converter.ConvertToSizes(wordsWithCount);
            var rectangles = layouter.GetRectangles(sizes);
            var imageSize = settings.ImageSize;
            if (imageSize == Size.Empty)
                imageSize = imageSizeCalculator.CalculateImageSize(rectangles);
            rectangles = rectanglesTransformer.TransformRectangles(rectangles, imageSize);
            words = wordsWithCount.Select(e => e.Word);
            var wordRectangles = words.Zip(rectangles, (w, r) => new WordRectangle(w, r)).ToList();
            var image = visualizer.DrawImage(wordRectangles, imageSize);
            saver.SaveImage(image, settings.OutputFilePath);
        }
    }
}
