using System.Collections.Generic;
using System.Linq;

namespace TagCloud.TextProcessing
{
    internal class TextProcessor : ITextProcessor
    {
        private readonly MyStemManager _myStemManager;
        private readonly IFileProvider _textProvider;

        public TextProcessor(IFileProvider textProvider, MyStemManager myStemManager)
        {
            _textProvider = textProvider;
            _myStemManager = myStemManager;
        }

        public IEnumerable<Dictionary<string, int>> GetWordsWithFrequency(ITextProcessingOptions options)
        {
            return options.FilesToProcess
                .Select(filePath => _myStemManager.GetMyStemResultData(_textProvider.GetTxtFilePath(filePath))).Select(
                    myStemResults => myStemResults
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