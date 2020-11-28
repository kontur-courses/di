using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CommandLine;
using TagCloud.Core;
using TagCloud.Core.FileReaders;
using TagCloud.Core.ImageCreators;
using TagCloud.Core.ImageSavers;
using TagCloud.Core.LayoutAlgorithms;
using TagCloud.Core.WordsProcessors;
using TagCloudUI.Extensions;

namespace TagCloudUI.UI
{
    public class ConsoleUI : IUserInterface
    {
        private readonly List<IFileReader> readers;
        private readonly IWordsProcessor wordsProcessor;
        private readonly ILayoutAlgorithm layoutAlgorithm;
        private readonly IImageCreator imageCreator;
        private readonly IImageSaver imageSaver;

        public ConsoleUI(IEnumerable<IFileReader> readers,
            IWordsProcessor wordsProcessor,
            ILayoutAlgorithm layoutAlgorithm,
            IImageCreator imageCreator,
            IImageSaver imageSaver)
        {
            this.readers = readers.ToList();
            this.wordsProcessor = wordsProcessor;
            this.layoutAlgorithm = layoutAlgorithm;
            this.imageCreator = imageCreator;
            this.imageSaver = imageSaver;
        }

        public void Run(IEnumerable<string> args)
        {
            Parser.Default
                .ParseArguments<Options>(args)
                .WithParsed(Run);
        }

        private void Run(Options options)
        {
            var processedWords = wordsProcessor
                .Process(GetWordsFromFile(options.InputPath))
                .MostFrequent(options.WordsCount)
                .ToList();
            var tags = CreateTags(processedWords, options.FontName).ToList();

            using var bitmap = imageCreator.Create(tags, options.FontName,
                layoutAlgorithm.GetLayoutSize());
            var savedPath = imageSaver.Save(bitmap, options.OutputPath, options.ImageFormat);
            Console.WriteLine($"Tag cloud visualization saved to: {savedPath}");
        }

        private IEnumerable<string> GetWordsFromFile(string inputPath)
        {
            var reader = readers
                .FirstOrDefault(r => r.IsValidExtension(Path.GetExtension(inputPath)));

            if (reader == null)
                throw new ArgumentException(
                    $"Unable to read file with this extension: {Path.GetExtension(inputPath)}");

            return reader.ReadAllWords(inputPath);
        }

        private IEnumerable<Tag> CreateTags(IReadOnlyCollection<string> words, string fontName)
        {
            return words.Select((word, index) => CreateTag(word, index, words.Count, fontName));
        }

        private Tag CreateTag(string word, int index, int wordsCount, string fontName)
        {
            var fontSize = wordsCount + 10 - index;
            using var font = new Font(fontName, fontSize);
            var tagSize = TextRenderer.MeasureText(word, font);

            return new Tag(word, layoutAlgorithm.PutNextRectangle(tagSize), fontSize);
        }
    }
}