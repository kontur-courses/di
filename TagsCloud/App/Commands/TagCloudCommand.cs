using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloud.Infrastructure;

namespace TagsCloud.App.Commands
{
    public class TagCloudCommand : ICommand
    {
        private const int MaxFontSize = 80;
        private const int MinFontSize = 10;
        private readonly Func<Point, ILayoutAlgorithm> algorithmGenerator;
        private readonly FileReader fileReader;
        private readonly IFontFamilyProvider fontFamilyProvider;
        private readonly IImageSizeProvider imageSizeProvider;
        private readonly Func<ILayoutAlgorithm, TagCloudLayouter> layouterGenerator;
        private readonly TagCloudPainter tagCloudPainter;
        private readonly WordFrequency wordFrequency;

        public TagCloudCommand(
            TagCloudPainter tagCloudPainter,
            WordFrequency wordFrequency,
            IImageSizeProvider imageSizeProvider,
            IFontFamilyProvider fontFamilyProvider,
            Func<Point, ILayoutAlgorithm> algorithmGenerator,
            Func<ILayoutAlgorithm, TagCloudLayouter> layouterGenerator,
            FileReader fileReader)
        {
            this.tagCloudPainter = tagCloudPainter;
            this.wordFrequency = wordFrequency;
            this.imageSizeProvider = imageSizeProvider;
            this.fontFamilyProvider = fontFamilyProvider;
            this.algorithmGenerator = algorithmGenerator;
            this.layouterGenerator = layouterGenerator;
            this.fileReader = fileReader;
        }

        public string Name { get; } = "tagcloud";
        public string Description { get; }

        public void Execute(string[] args)
        {
            var placedWords = new List<Word>();
            var graphics = Graphics.FromImage(new Bitmap(1, 1));
            var wordFrequencies = wordFrequency.Get(fileReader.ReadLines(args[0]));
            var tagCloudLayouter = GetTagCloudLayouter();
            foreach (var (word, frequency) in wordFrequencies
                .OrderByDescending(x => x.Value))
            {
                var font = new Font(fontFamilyProvider.FontFamily, GetFontSize(frequency));
                var wordSize = graphics.MeasureString(word, font).ToSize();
                var wordRectangle = tagCloudLayouter.PutNextRectangle(wordSize);
                placedWords.Add(new Word(word, font, wordRectangle));
            }

            tagCloudPainter.Paint(placedWords);
        }

        private int GetFontSize(double frequency)
        {
            return Math.Max((int) Math.Round(MaxFontSize * frequency), MinFontSize);
        }

        private TagCloudLayouter GetTagCloudLayouter()
        {
            var center = imageSizeProvider.ImageSize.GetCenter();
            var algorithm = algorithmGenerator(center);
            return layouterGenerator(algorithm);
        }
    }
}