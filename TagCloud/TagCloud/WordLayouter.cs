using System.Collections.Generic;
using System.Drawing;

namespace TagCloud
{
    public class WordLayouter
    {
        public List<Word> Layout(ICloudLayouter layouter, Dictionary<string, int> wordsWithFrequency)
        {
            var g = Graphics.FromImage(new Bitmap(1, 1));
            var listWords = new List<Word>();
            foreach (var pair in wordsWithFrequency)
            {
                var word = pair.Key;
                var font = new Font(FontFamily.GenericSerif, pair.Value > 1 ? (int)(pair.Value * 14 * 0.6) : 14);
                var wordSize = g.MeasureString(word, font).ToSize();
                var wordRectangle = layouter.PutNextRectangle(wordSize);
                listWords.Add(new Word(word, font, wordRectangle));
            }

            return listWords;
        }
    }
}