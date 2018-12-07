using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Drawing;
using TagsCloudContainer.Extensions;

namespace TagsCloudContainer.Layout
{
    public class WordLayout
    {
        private readonly IRectangleLayout layout;
        private readonly IWriter writer;
        public ImageSettings Settings;

        private readonly Dictionary<string, (Rectangle, float)> wordRectangles;
        public ImmutableDictionary<string, (Rectangle, float)> WordRectangles => wordRectangles.ToImmutableDictionary();

        public WordLayout(IRectangleLayout layout, IWriter writer, ImageSettings settings)
        {
            this.layout = layout;
            this.writer = writer;
            Settings = settings;

            wordRectangles = new Dictionary<string, (Rectangle, float)>();
        }

        public void PlaceWords(Dictionary<string, int> wordWeights)
        {
            var weightSum = wordWeights.Sum(p => p.Value);
            wordWeights.ToList().Sort((p1, p2) => p1.Value.CompareTo(p2.Value));

            foreach (var pair in wordWeights)
            {
                var fontSize = (float) pair.Value / weightSum * Settings.MaxFontSize;
                var newSize = GetWordSize(pair.Key, fontSize);

                var rectangle = layout.PutNextRectangle(newSize);
                wordRectangles.Add(pair.Key, (rectangle, fontSize));
            }
        }

        private Size GetWordSize(string word, float fontSize)
        {
            var font = Settings.TextFont.SetSize(fontSize);
            var size = writer.Graphics.MeasureString(word, font);
            return new Size((int) size.Width, (int) size.Height);
        }
    }
}