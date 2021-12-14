using System.Linq;

namespace Visualization.Preprocessors
{
    public class CombinedPreprocessor : IWordsPreprocessor
    {
        private readonly IWordsPreprocessor[] childPreprocessors;

        public CombinedPreprocessor(IWordsPreprocessor[] childPreprocessors)
        {
            this.childPreprocessors = childPreprocessors;
        }

        public string[] Preprocess(string[] rawWords)
        {

            return childPreprocessors
                .Aggregate(rawWords, (current, preprocessor) => preprocessor.Preprocess(current));
        }
    }
}