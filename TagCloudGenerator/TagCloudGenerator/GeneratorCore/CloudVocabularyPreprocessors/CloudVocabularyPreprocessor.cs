using System.Collections.Generic;

namespace TagCloudGenerator.GeneratorCore.CloudVocabularyPreprocessors
{
    public abstract class CloudVocabularyPreprocessor
    {
        private readonly CloudVocabularyPreprocessor nextPreprocessor;

        protected CloudVocabularyPreprocessor(CloudVocabularyPreprocessor nextPreprocessor) =>
            this.nextPreprocessor = nextPreprocessor;

        public IEnumerable<string> Process(IEnumerable<string> words)
        {
            words = ProcessVocabulary(words);

            return !(nextPreprocessor is null)
                       ? nextPreprocessor.Process(words)
                       : words;
        }

        protected abstract IEnumerable<string> ProcessVocabulary(IEnumerable<string> words);
    }
}