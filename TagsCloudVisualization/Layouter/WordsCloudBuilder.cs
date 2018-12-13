using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.WordsProcessing;

namespace TagsCloudVisualization.Layouter
{
    public class WordsCloudBuilder : IWordsCloudBuilder
    {
        private readonly ICloudLayouter layouter;
        private readonly IWordsProvider wordsProvider;
        private readonly ISizeConverter sizeConverter;
        private readonly IWeighter weighter;

        public WordsCloudBuilder(IWordsProvider wordsProvider, ICloudLayouter layouter, ISizeConverter sizeConverter, IWeighter weighter)
        {
            this.wordsProvider = wordsProvider;
            this.layouter = layouter;
            this.sizeConverter = sizeConverter;
            this.weighter = weighter;
        }

        public IEnumerable<Word> Build()
        {
            var words = wordsProvider.Provide();
            var weightedWords = weighter.WeightWords(words);
            return sizeConverter.Convert(weightedWords).Select(PutNextWord);
        }

        private Word PutNextWord(SizedWord sizedWord)
        {
            return new Word(sizedWord.Word, sizedWord.Font, layouter.PutNextRectangle(sizedWord.Size));
        }

    }
}
