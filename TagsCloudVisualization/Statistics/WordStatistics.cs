using System;
using System.Collections.Generic;
using System.Linq;
using ResultProject;
using TagsCloudVisualization.WordProcessors;

namespace TagsCloudVisualization.Statistics
{
    public class BaseWordsStatistics : IWordsStatistics
    {
        private readonly IDictionary<string, int> statistics = new Dictionary<string, int>();
        protected readonly ITextProcessor textProcessor;
        private string? error;

        public BaseWordsStatistics(ITextProcessor textProcessor)
        {
            this.textProcessor = textProcessor;
        }

        private void AddProcessedWord(string word)
        {
            if (string.IsNullOrWhiteSpace(word) || string.IsNullOrEmpty(word)) return;
            statistics[word.ToLower()] = 1 + (statistics.TryGetValue(word.ToLower(), out var count) ? count : 0);
        }

        public void AddWords(IEnumerable<string> word)
        {
            textProcessor.AsResult()
                .Then(x => x.ProcessWords(word))
                .ThenForEach(x =>
                {
                    AddProcessedWord(x);
                    return new Result<None>();
                })
                .ThrowOnFail();
        }
        
        public virtual Result<IEnumerable<WordCount>> GetStatistics()
        {
            return statistics.AsResult()
                .ThenFailIf(x => !x.Any(), "No words in statistic")
                .ThenForEach<string, int, WordCount>(x => WordCount.Create(x))
                .Then(x => x.OrderByDescending(wordCount => wordCount.Count)
                            .ThenBy(wordCount => wordCount.Word) as IEnumerable<WordCount>);
        }

        public virtual IWordsStatistics CreateStatistics() 
            => new BaseWordsStatistics(textProcessor);

        public virtual Result<IEnumerable<WordCount>> GetStatistics(uint topWordCount)
        {
            return GetStatistics().Then(x => x.Take((int)topWordCount));
        }
    }
}