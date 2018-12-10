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
        private readonly IWeighter weighter;

        public WordsCloudBuilder(ICloudLayouter layouter, ISizeConverter sizeConverter, IWeighter weighter)
        {
            this.layouter = layouter;
            this.sizeConverter = sizeConverter;
            this.weighter = weighter;
        }

        public IEnumerable<Word> Build()
        {
            return sizeConverter.Convert(weighter.WeightWords()).Select(PutNextWord);
        }

        private Word PutNextWord(SizedWord sizedWord)
        {
            if (sizedWord.Word.Length == 0)
                throw new ArgumentException("text length should be grater than zero");
            return new Word(sizedWord.Word, sizedWord.Font, layouter.PutNextRectangle(sizedWord.Size));
        }

    }
}
