using System.Collections.Generic;
using System.Linq;

namespace TagCloudGenerator.GeneratorCore.CloudVocabularyPreprocessors
{
    public class ExcludingPreprocessors : CloudVocabularyPreprocessor
    {
        private readonly TagCloudContext cloudContext;

        // used implicitly by Program.BuildContainer
        // ReSharper disable once UnusedMember.Global
        public ExcludingPreprocessors(CloudContextGenerator contextGenerator) : this(null, contextGenerator) { }

        private ExcludingPreprocessors(CloudVocabularyPreprocessor nextPreprocessor,
                                       CloudContextGenerator contextGenerator) : base(nextPreprocessor) =>
            cloudContext = contextGenerator.GetTagCloudContext();

        protected override IEnumerable<string> ProcessVocabulary(IEnumerable<string> words) =>
            words.Where(word => !cloudContext.ExcludedWords.Contains(word));
    }
}