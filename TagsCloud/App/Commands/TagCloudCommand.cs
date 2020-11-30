using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloud.Infrastructure;

namespace TagsCloud.App.Commands
{
    public class TagCloudCommand : ICommand
    {
        private readonly Func<Point, ILayoutAlgorithm> algorithmGenerator;
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
            Func<ILayoutAlgorithm, TagCloudLayouter> layouterGenerator)
        {
            this.tagCloudPainter = tagCloudPainter;
            this.wordFrequency = wordFrequency;
            this.imageSizeProvider = imageSizeProvider;
            this.fontFamilyProvider = fontFamilyProvider;
            this.algorithmGenerator = algorithmGenerator;
            this.layouterGenerator = layouterGenerator;
        }

        public string Category { get; }
        public string Name { get; } = "tagcloud";
        public string Description { get; }

        public void Execute(string[] args)
        {
            var placedWords = new List<Word>();
            var wordFrequencies = wordFrequency.GetFromFile(args[0]);
            var tagCloudLayouter = GetTagCloudLayouter();
            foreach (var (word, frequency) in wordFrequencies
                .OrderBy(x => x.Value))
            {
                var font = new Font(fontFamilyProvider.FontFamily, GetFontSize(frequency, imageSizeProvider.ImageSize));
                var wordSize = Graphics.FromImage(new Bitmap(1, 1)).MeasureString(word, font).ToSize();
                var wordPoint = tagCloudLayouter.PutNextRectangle(wordSize);
                placedWords.Add(new Word(word, font, wordPoint));
            }

            tagCloudPainter.Paint(placedWords);
        }

        private int GetFontSize(double wordFrequency, ImageSize imageSize)
        {
            //todo
            return 20;
        }

        private TagCloudLayouter GetTagCloudLayouter()
        {
            var center = imageSizeProvider.ImageSize.GetCenter();
            var algorithm = algorithmGenerator(center);
            return layouterGenerator(algorithm);
        }
    }
}