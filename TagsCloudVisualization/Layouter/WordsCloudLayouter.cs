using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization
{
    public class WordsCloudLayouter : IWordsCloudLayouter
    {
        private readonly ICloudLayouter layouter;

        public WordsCloudLayouter(ICloudLayouter layouter)
        {
            this.layouter = layouter;
        }

        public IEnumerable<Word> LayWords(IEnumerable<SizedWord> sizedWords)
        {
            return sizedWords.Select(PutNextWord);
        }

        private Word PutNextWord(SizedWord sizedWord)
        {
            if (sizedWord.Word.Length == 0)
                throw new ArgumentException("text length should be grater than zero");
            return new Word(sizedWord.Word, sizedWord.Font, layouter.PutNextRectangle(sizedWord.Size));
        }

    }
}
