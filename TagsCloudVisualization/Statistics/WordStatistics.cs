using System.Collections.Generic;
using System.Linq;
using ResultProject;
using TagsCloudVisualization.WordProcessors;

namespace TagsCloudVisualization.Statistics
{
    public class BaseWordsStatistics : IWordsStatistics
    {
        private readonly IDictionary<string, int> statistics = new Dictionary<string, int>();
        private readonly ITextProcessor textProcessor;

        public BaseWordsStatistics(ITextProcessor textProcessor)
        {
            this.textProcessor = textProcessor;
        }

        private void AddProcessedWord(string word)
        {
            if (string.IsNullOrWhiteSpace(word)) return;
            statistics[word.ToLower()] = 1 + (statistics.TryGetValue(word.ToLower(), out var count) ? count : 0);
        }

        public void AddWords(IEnumerable<string> word)
        {
            foreach (var processWord in textProcessor.ProcessWords(word))
            {
                AddProcessedWord(processWord);
            }
        }
        
        public virtual Result<IEnumerable<WordCount>> GetStatistics()
        {
            if (!statistics.Any()) return Result.Fail<IEnumerable<WordCount>>("No words in statistic");
            
            return Result.Ok(statistics
                .Select(WordCount.Create)
                .OrderByDescending(wordCount => wordCount.Count)
                .ThenBy(wordCount => wordCount.Word) as IEnumerable<WordCount>);
        }

        public virtual Result<IEnumerable<WordCount>> GetStatistics(uint topWordCount)
        {
            return GetStatistics().Then(x => x.Take((int)topWordCount));
        }
    }
}