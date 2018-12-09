using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.WordsProcessing;

namespace TagsCloudVisualization.Layouter
{
    public class WordsCloudBuilder : IWordsCloudBuilder
    {
        private readonly ICloudLayouter layouter;
        private readonly ISizeConverter sizeConverter;

        public WordsCloudBuilder(ICloudLayouter layouter, ISizeConverter sizeConverter)
        {
            this.layouter = layouter;
            this.sizeConverter = sizeConverter;
        }

        public IEnumerable<Word> Build()
        {
            return sizeConverter.Convert().Select(PutNextWord);
        }

        private Word PutNextWord(SizedWord sizedWord)
        {
            if (sizedWord.Word.Length == 0)
                throw new ArgumentException("text length should be grater than zero");
            return new Word(sizedWord.Word, sizedWord.Font, layouter.PutNextRectangle(sizedWord.Size));
        }

    }
}
