using System;
using System.Linq;
using TagsCloudVisualization.WordReaders.WordProcessors;
using TagsCloudVisualization.WordReaders.WordValidators;

namespace TagsCloudVisualization.WordReaders
{
    public class ProcessedWordReader : IWordReader
    {
        private readonly IWordReader inner;
        private readonly IWordProcessor[] wordProcessors;
        private readonly IWordValidator[] wordValidators;
        private string? nextWord;
        
        public ProcessedWordReader(IWordReader inner, IWordProcessor[] wordProcessors, IWordValidator[] wordValidators)
        {
            this.inner = inner;
            this.wordProcessors = wordProcessors;
            this.wordValidators = wordValidators;
            LoadNextValidWord();
        }

        private void LoadNextValidWord()
        {
            do
            {
                if (!inner.HasWord())
                {
                    nextWord = null;
                    return;
                }
                nextWord = inner.Read();
            } while (wordValidators.All(x => x.Validate(nextWord)));
        }

        public string Read()
        {
            if (!HasWord()) throw new InvalidOperationException("has no word anymore");
            var word = nextWord!;
            LoadNextValidWord();
            return wordProcessors.Aggregate(word, (current, wordProcessor) => wordProcessor.ProcessWord(current));
        }

        public bool HasWord()
        {
            return nextWord is not null;
        }
    }
}