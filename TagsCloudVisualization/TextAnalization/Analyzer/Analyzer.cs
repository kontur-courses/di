using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeCantSpell.Hunspell;

namespace TagsCloudVisualization.TextAnalization.Analyzer
{
    public class Analyzer : IAnalyzer
    {
        public readonly IEnumerable<PartsOfSpeech.SpeechPart> SpeechParts;

        public Analyzer(IEnumerable<PartsOfSpeech.SpeechPart> speechParts)
        {
            SpeechParts = speechParts;
        }

        private bool CheckWord(string word)
        {
            //здесь наверное стоит проверить на правильность слова по какому-нибудь словарю


            /*
            if (_speechParts.Contains(CheckPartOfSpeech(word)))
                return word;
            */

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
                {
                    yield return new Word(word);
                }
            }
        }
    }
}
