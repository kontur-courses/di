using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Algorithm.Layouting;
using TagsCloudContainer.Algorithm.Organizing;
using TagsCloudContainer.Algorithm.SizeProviding;
using TagsCloudContainer.Algorithm.WeightSetting;

namespace TagsCloudContainer.Algorithm
{
    public class LayoutAlgorithm : ILayoutAlgorithm
    {
        private readonly IWordWeightSetter wordWeightSetter;
        private readonly IWordSizeProvider wordSizeProvider;
        private readonly IWordOrganizer wordOrganizer;
        private readonly ILayouter layouter;

        public LayoutAlgorithm(IWordWeightSetter wordWeightSetter, IWordSizeProvider wordSizeProvider,
            IWordOrganizer wordOrganizer, ILayouter layouter)
        {
            this.wordWeightSetter = wordWeightSetter;
            this.wordSizeProvider = wordSizeProvider;
            this.wordOrganizer = wordOrganizer;
            this.layouter = layouter;
        }

        public IEnumerable<(string, Rectangle)> GetLayout(IEnumerable<string> words)
        {
            var convertedWords = words.Select(w => new Word {Value = w});
            var weightedWords = wordWeightSetter.SetWordsWeights(convertedWords);
            var wordsWithSize = wordSizeProvider.SetWordsSizes(weightedWords);
            var orderedWords = wordOrganizer.GetSortedWords(wordsWithSize);

            foreach (var word in orderedWords)
            {
                var rectangleForWord = layouter.PutNextRectangle(word.Size);
                yield return (word.Value, rectangleForWord);
            }
        }
    }
}