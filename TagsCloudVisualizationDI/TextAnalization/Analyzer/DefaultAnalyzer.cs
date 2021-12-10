using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualizationDI.TextAnalization.Analyzer
{
    public class DefaultAnalyzer : IAnalyzer
    {
        private readonly HashSet<PartsOfSpeech.SpeechPart> _excludedSpeechParts;
        private readonly IEnumerable<string> _excludedWords;


        public DefaultAnalyzer(IEnumerable<PartsOfSpeech.SpeechPart> excludedSpeechParts, IEnumerable<string> excludedWords)
        {
            _excludedSpeechParts = excludedSpeechParts.ToHashSet();
            _excludedWords = excludedWords;
        }

        private bool CheckWord(string inputWord, out string wordContent, out PartsOfSpeech.SpeechPart enumElementOfCurrentType)
        {
            var wordAndPart = inputWord.Split(new []{' ', ',', '='}, 3, StringSplitOptions.RemoveEmptyEntries);
            if (wordAndPart.Length < 2)
            {
                wordContent = default;
                enumElementOfCurrentType = default;
                return false;
            }

            wordContent = wordAndPart[0];
            var type = wordAndPart[1];
            enumElementOfCurrentType = (PartsOfSpeech.SpeechPart)Enum.Parse(typeof(PartsOfSpeech.SpeechPart), type);
            return (inputWord.Split(' ').Length == 1);
        }

        private bool IsNotExcludedPart(PartsOfSpeech.SpeechPart enumElementOfCurrentType)
        {
            var excludedParts = _excludedSpeechParts;
            if (!excludedParts.Contains(enumElementOfCurrentType))
                return true;

            return false;
        }

        public IEnumerable<Word> GetAnalyzedWords(IEnumerable<string> words)
        {
            foreach (var word in words)
            {
                if (CheckWord(word, out string content, out PartsOfSpeech.SpeechPart type))
                {
                    if (IsNotExcludedPart(type) && IsNotExcludedWord(content))
                        yield return new Word(content);
                }
            }
        }

        private bool IsNotExcludedWord(string word)
        {
            return !_excludedWords.Contains(word);
        }
    }
}
