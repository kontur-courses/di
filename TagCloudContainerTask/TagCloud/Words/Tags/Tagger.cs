using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TagCloud.Words.Tags
{
    public class Tagger : ITagger
    {
        private const float MinEmSize = 12;
        private const float MaxEmSize = 36;

        public IEnumerable<Tag> CreateRawTags(Dictionary<string, double> wordsFrequencies, string fontName)
        {
            Tag.WordFontName = fontName;

            foreach (var word in wordsFrequencies.Keys)
            {
                var wordEmSize = GetScaledWordEmSize(wordsFrequencies[word]);

                var wordOuterRectangle = GetWordOuterRectangle(word, fontName, wordEmSize);

                yield return new Tag(word, wordEmSize, wordOuterRectangle);
            }
        }

        private float GetScaledWordEmSize(double wordFrequency)
        {
            return MinEmSize + (float)Math.Round((MaxEmSize - MinEmSize) * wordFrequency);
        }

        private Rectangle GetWordOuterRectangle(string word, string fontName, float wordEmSize)
        {
            return new Rectangle(Point.Empty, GetWordOuterRectangleSize(word, fontName, wordEmSize));
        }

        private Size GetWordOuterRectangleSize(string word, string fontName, float wordEmSize)
        {
            using (var font = new Font(fontName, wordEmSize))
            {
                return TextRenderer.MeasureText(word, font);
            }
        }
    }
}