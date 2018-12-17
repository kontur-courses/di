using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Drawing;
using TagsCloudContainer.Extensions;

namespace TagsCloudContainer.Layout
{
    public class WordLayout
    {
        private readonly IRectangleLayout layout;
        public readonly Graphics Graphics;
        public readonly ImageSettings Settings;
        private Dictionary<string, (Rectangle, float)> words;

        public IReadOnlyDictionary<string, (Rectangle, float)> WordRectangles => words;

        public WordLayout(IRectangleLayout layout, Graphics graphics, ImageSettings settings)
        {
            this.layout = layout;
            Graphics = graphics;
            Settings = settings;
            words = new Dictionary<string, (Rectangle, float)>();
        }

        public void PlaceWords(Dictionary<string, int> wordWeights)
        {
            var weightSum = wordWeights.Sum(p => p.Value);
            wordWeights.ToList().Sort((p1, p2) => p1.Value.CompareTo(p2.Value));

            foreach (var pair in wordWeights)
            {
                var fontSize = (float) pair.Value / weightSum * Settings.MaxFontSize;
                fontSize = Math.Max(fontSize, Settings.MinFontSize);

                var newSize = GetWordSize(pair.Key, fontSize);

                var rectangle = layout.PutNextRectangle(newSize);
                words.Add(pair.Key, (rectangle, fontSize));
            }
        }

        private Size GetWordSize(string word, float fontSize)
        {
            var font = Settings.TextFont.SetSize(fontSize);
            var size = Graphics.MeasureString(word, font);
            return new Size((int) size.Width + 5, (int) size.Height);
        }
    }
}