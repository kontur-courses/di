using System.Collections.Generic;

namespace TagsCloudVisualizationDI.TextAnalization.Analyzer
{
    public class DefaultAnalyzer : IAnalyzer
    {
        public readonly IEnumerable<PartsOfSpeech.SpeechPart> SpeechParts;

        public DefaultAnalyzer(IEnumerable<PartsOfSpeech.SpeechPart> speechParts)
        {
            SpeechParts = speechParts;
        }

        private bool CheckWord(string word)
        {
            return (word.Split(' ').Length == 1);
        }

        private PartsOfSpeech.SpeechPart CheckPartOfSpeech(string word)
        {
            //здесь можно проверить часть речи
            return PartsOfSpeech.SpeechPart.Noun;
        }

        public IEnumerable<Word> GetAnalyzedWords(IEnumerable<string> words)
        {
            foreach (var word in words)
            {
                if (CheckWord(word))
                    yield return new Word(word);
            }
        }
    }
}
