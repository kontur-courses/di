using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using CommandLine;
using TagCloud.Analyzers;
using TagCloud.Creators;
using TagCloud.Layouters;
using TagCloud.Readers;
using TagCloud.Visualizers;
using TagCloud.Writers;

namespace TagCloud.UI.Console
{
    public class ConsoleUI : IUserInterface
    {
        private readonly IFileReaderFactory readerFactory;
        private readonly ITextAnalyzer textAnalyzer;
        private readonly BoringWordsFilter boringWordsFilter;
        private readonly ICloudLayouterFactory layouterFactory;
        private readonly IVisualizer visualizer;
        private readonly IFileWriter writer;
        private readonly ITagCreatorFactory tagCreatorFactory;

        public ConsoleUI(IFileReaderFactory readerFactory,
            ITextAnalyzer textAnalyzer,
            BoringWordsFilter boringWordsFilter,
            ICloudLayouterFactory layouterFactory,
            IVisualizer visualizer,
            IFileWriter writer, 
            ITagCreatorFactory tagCreatorFactory)
        {
            this.readerFactory = readerFactory;
            this.textAnalyzer = textAnalyzer;
            this.layouterFactory = layouterFactory;
            this.visualizer = visualizer;
            this.writer = writer;
            this.tagCreatorFactory = tagCreatorFactory;
            this.boringWordsFilter = boringWordsFilter;
        }

        public void Run(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(Run)
                .WithNotParsed(HandleParseError);
        }

        private void Run(Options options)
        {
            var drawingSettings = GetDrawingSettings(options);
            var fileExtension = GetExtensionsFromFileName(options.InputFilename);
            
            var reader = readerFactory.Create(fileExtension);

            var text = reader.ReadFile(options.InputFilename);
            var wordsToExclude = reader.ReadFile(options.ExcludedWordsFile).ToHashSet();
            //var words = textAnalyzer.Analyze(text, wordsToExclude);
            //var wordFrequencies = frequencyAnalyzer.Analyze(words);
            boringWordsFilter.AddWords(wordsToExclude);
            var wordFrequencies = textAnalyzer.Analyze(text);

            var tags = tagCreatorFactory
                .Create(drawingSettings.Font)
                .Create(wordFrequencies);

            var center = new Point(options.Width / 2, options.Height / 2);
            var placedTags = layouterFactory.Create(center).PutTags(tags);
            using (drawingSettings)
            {
                var image = visualizer.DrawCloud(placedTags, drawingSettings);
                writer.Write(image, options.OutputFilename, ImageFormat.Png);
            }
        }

        private string GetExtensionsFromFileName(string filename)
        {
            var lastDotIndex = filename.LastIndexOf('.');
            return filename[lastDotIndex..];
        }

        private static DrawingSettings GetDrawingSettings(Options options)
        {
            var backgroundColor = Color.FromName(options.BackgroundColor);
            var penColor = Color.FromName(options.WordColor);
            var font = new Font(options.FontName, options.FontSize);

            return new DrawingSettings(penColor, 
                backgroundColor, 
                options.Width, 
                options.Height, 
                font);
        }

        private static void HandleParseError(IEnumerable<Error> errors)
        {
            foreach (var error in errors)
            {
                if (error.StopsProcessing)
                    throw new ArgumentException($"Wrong command-line arguments. {error}");
            }
        }
    }
}
