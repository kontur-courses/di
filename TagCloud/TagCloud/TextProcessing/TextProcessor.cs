using System.Collections.Generic;
using System.Linq;

namespace TagCloud.TextProcessing
{
    internal class TextProcessor : ITextProcessor
    {
        private readonly IFileProvider _textProvider;

        public TextProcessor(IFileProvider textProvider)
        {
            _textProvider = textProvider;
        }

        public IEnumerable<Dictionary<string, int>> GetWordsWithFrequency(ITextProcessingOptions options)
        {
            return options.FilesToProcess
                .Select(filePath => MyStemManager.GetMyStemResultData(_textProvider.GetTxtFilePath(filePath)))
                .Select(myStemResults => myStemResults
                        .Where(r => !options.ExcludePartOfSpeech.Contains(r.PartOfSpeech)
                                    || options.IncludeWords.Contains(r.Lemma))
                        .Select(r => r.Lemma)
                        .Where(w => !options.ExcludeWords.Contains(w))
                        .GroupBy(s => s)
                        .OrderByDescending(g => g.Count())
                        .Take(options.Amount)
                        .ToDictionary(g => g.Key, g => g.Count()));
        }
    }
}