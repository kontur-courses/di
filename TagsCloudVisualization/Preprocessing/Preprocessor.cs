using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.Preprocessing
{
    public class Preprocessor
    {
        private readonly IFilter[] filters;
        private readonly IWordTransformer[] transformers;

        public Preprocessor(IFilter[] filters, IWordTransformer[] transformers)
        {
            this.filters = filters;
            this.transformers = transformers;
        }

        public IEnumerable<string> Preprocess(IEnumerable<string> words)
        {
            foreach (var filter in filters)
                words = filter.FilterWords(words);

            return words.Select(ApplyAllTransforms);
        }

        private string ApplyAllTransforms(string word)
        {
            var transformedWord = word;
            foreach (var transformer in transformers)
                transformedWord = transformer.TransformWord(transformedWord);
            return transformedWord;
        }
    }
}
