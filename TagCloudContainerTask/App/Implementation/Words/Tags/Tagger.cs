using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using App.Infrastructure.SettingsHolders;
using App.Infrastructure.Words.Tags;

namespace App.Implementation.Words.Tags
{
    public class Tagger : ITagger
    {
        private const float MinEmSize = 12;
        private const float MaxEmSize = 36;

        private readonly IFontSettingsHolder fontSettings;

        public Tagger(IFontSettingsHolder fontSettings)
        {
            this.fontSettings = fontSettings;
        }

        public IEnumerable<Tag> CreateRawTags(Dictionary<string, double> wordsFrequencies)
        {
            Tag.WordFont = fontSettings.Font;

            foreach (var word in wordsFrequencies.Keys)
            {
                var wordEmSize = GetScaledWordEmSize(wordsFrequencies[word]);

                var wordOuterRectangle = GetWordOuterRectangle(word, wordEmSize);

                yield return new Tag(word, wordEmSize, wordOuterRectangle);
            }
        }

        private float GetScaledWordEmSize(double wordFrequency)
        {
            return MinEmSize + (float)Math.Round((MaxEmSize - MinEmSize) * wordFrequency);
        }

        private Rectangle GetWordOuterRectangle(string word, float wordEmSize)
        {
            return new Rectangle(Point.Empty, GetWordOuterRectangleSize(word, wordEmSize));
        }

        private Size GetWordOuterRectangleSize(string word, float wordEmSize)
        {
            using (var font = new Font(fontSettings.Font.Name, wordEmSize))
            {
                return TextRenderer.MeasureText(word, font);
            }
        }
    }
}