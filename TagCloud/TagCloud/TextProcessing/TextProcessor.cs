using System.Collections.Generic;
using System.Linq;

namespace TagCloud.TextProcessing
{
    internal class TextProcessor : ITextProcessor
    {
        private readonly IMorphologyAnalyzer _morphologyAnalyzer;
        private readonly IFileProvider _textProvider;

        public TextProcessor(IFileProvider textProvider, IMorphologyAnalyzer morphologyAnalyzer)
        {
            _textProvider = textProvider;
            _morphologyAnalyzer = morphologyAnalyzer;
        }

        public IEnumerable<Dictionary<string, int>> GetWordsWithFrequency(ITextProcessingOptions options)
        {
            return options.FilesToProcess
                .Select(filePath => _morphologyAnalyzer.GetLexemesFrom(_textProvider.GetTxtFilePath(filePath)))
                .Select(lexemes => lexemes.Where(r =>
                    !options.ExcludePartOfSpeech.Contains(r.PartOfSpeech) || options.IncludeWords.Contains(r.Lemma)))
                .Select(withPosExcluded => withPosExcluded.Select(r => r.Lemma))
                .Select(lemmas => lemmas.Where(w => !options.ExcludeWords.Contains(w)))
                .Select(withWordsExcluded => GetMostCommonWords(withWordsExcluded, options.Amount));
        }

        private static Dictionary<string, int> GetMostCommonWords(IEnumerable<string> words, int amount)
        {
            return words
                .GroupBy(s => s)
                .OrderByDescending(g => g.Count())
                .Take(amount)
                .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}