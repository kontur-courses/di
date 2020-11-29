using System.Collections.Generic;

namespace TagsCloudContainer.WordsParser
{
    public class WordsAnalyzer: IWordsAnalyzer
    {
        private readonly ISettings settings;
        private readonly IWordReader wordReader;

        public WordsAnalyzer(ISettings settings, IWordReader wordReader)
        {
            this.settings = settings;
            this.wordReader = wordReader;
        }

        public Dictionary<string, int> AnalyzeWords()
        {
            var wordsCount = new Dictionary<string, int>();

            while (true)
            {
                var word = wordReader.ReadWord();
                if (word is null)
                    break;
                word = NormalizeWord(word);
                wordsCount.SetOrUpdate(word);
            }

            return wordsCount;
        }

        private static string NormalizeWord(string word)
        {
            return word.ToLower();
        }
    }
}