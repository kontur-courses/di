using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.WordsProcessing;

namespace TagsCloudVisualization.Layouter
{
    public class WordsCloudLayouter : IWordsCloudLayouter
    {
        private readonly ICloudLayouter layouter;
        private readonly ISizer sizer;

        public WordsCloudLayouter(ICloudLayouter layouter, ISizer sizer)
        {
            this.layouter = layouter;
            this.sizer = sizer;
        }

        public IEnumerable<Word> LayWords()
        {
            return sizer.SizeWords().Select(PutNextWord);
        }

        private Word PutNextWord(SizedWord sizedWord)
        {
            if (sizedWord.Word.Length == 0)
                throw new ArgumentException("text length should be grater than zero");
            return new Word(sizedWord.Word, sizedWord.Font, layouter.PutNextRectangle(sizedWord.Size));
        }

    }
}
