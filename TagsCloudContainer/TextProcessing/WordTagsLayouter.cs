using System;
using System.Collections.Generic;
using System.Drawing;
using RectanglesCloudLayouter.Interfaces;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.TextProcessing
{
    public class WordTagsLayouter : IWordTagsLayouter
    {
        private readonly IWordsFrequency _wordsFrequency;
        private readonly ICloudLayouter _cloudLayouter;
        private readonly IWordMeasurer _wordMeasurer;
        private readonly Font _font;

        public WordTagsLayouter(IWordsFrequency wordsFrequency, ICloudLayouter cloudLayouter,
            IWordMeasurer wordMeasurer, Font font)
        {
            _wordsFrequency = wordsFrequency;
            _cloudLayouter = cloudLayouter;
            _wordMeasurer = wordMeasurer;
            _font = font;
        }

        public IEnumerable<WordTag> GetWordTags(string text)
        {
            if (text == null)
                throw new Exception("String must be not null");
            var wordsAndFrequency = _wordsFrequency.GetWordsFrequency(text);
            foreach (var word in wordsAndFrequency.Keys)
            {
                var wordFont = new Font(_font.FontFamily,
                    _font.Size + (float) Math.Log2(wordsAndFrequency[word]) * 7);
                var wordSize = _wordMeasurer.GetWordSize(word, wordFont);
                var rectangle = _cloudLayouter.PutNextRectangle(wordSize);
                yield return new WordTag(word, rectangle, wordFont);
            }
        }
    }
}