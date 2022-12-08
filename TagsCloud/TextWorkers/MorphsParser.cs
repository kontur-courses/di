using DeepMorphy;
using System.Collections.Generic;
using TagsCloud.TextWorker;

namespace TagsCloud.TextWorkers
{
    public static class MorphsParser
    {
        public static Dictionary<string, int> GetMorphs(string filePath)
        {
            var morph = new MorphAnalyzer(true);

            var text = TextReader.ReadFile(filePath);

            var textSplitter = new TextSplitter();
            var words = textSplitter.SplitTextOnWords(text);

            var morphInfo = morph.Parse(words);

            var morphsFilter = new MorphsFilter();
            var clearMorphs = morphsFilter.FilterRedutantWords(morphInfo);

            var normalFormWords = NormalFormParser.ConvertWordsToNormalForm(clearMorphs);

            var wordsFrequency = WordsFrequencyAnalizer.GetSortedDictOfWordsFreq(normalFormWords);

            return wordsFrequency;
        }
    }
}
