using System.Collections.Generic;
using System.Linq;

namespace TagCloudGenerator.GeneratorCore.CloudVocabularyPreprocessors
{
    // instantiated implicitly by Program.BuildContainer
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ToLowerPreprocessor : CloudVocabularyPreprocessor
    {
        public ToLowerPreprocessor(CloudVocabularyPreprocessor nextPreprocessor) : base(nextPreprocessor) { }

        protected override IEnumerable<string> ProcessVocabulary(IEnumerable<string> words) =>
            words.Select(word => word.ToLower());
    }
}