using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TagCloud.Words.Tagging
{
    public class WordToTagToTagWrapper : IWordToTagWrapper
    {
        private const float MinKegelSize = 10;
        private const float MaxKegelSize = 30;

        public Dictionary<string, Tag> WrapWords(
            Dictionary<string, double> wordsFrequencies, Font font)
        {
            var wordsSizes = new Dictionary<string, Tag>();

            foreach (var word in wordsFrequencies.Keys)
            {
                var wordFrequency = wordsFrequencies[word];
                var actualWordSize = MinKegelSize + (float)Math.Round((MaxKegelSize - MinKegelSize) * wordFrequency);

                var fnt = new Font(font.Name, actualWordSize);
                var wordRectangle = TextRenderer.MeasureText(word, fnt);

                var tag = new Tag(word, actualWordSize, wordRectangle);

                wordsSizes[word] = tag;
            }

            return wordsSizes;
        }
    }
}