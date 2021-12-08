using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualizationDI.TextAnalization.Analyzer
{
    public class DefaultAnalyzer : IAnalyzer
    {
        public readonly IEnumerable<PartsOfSpeech.SpeechPart> ExcludedSpeechParts;

        //public readonly PartsOfSpeech.SpeechPart;

        public DefaultAnalyzer(IEnumerable<PartsOfSpeech.SpeechPart> excludedSpeechParts)
        {
            ExcludedSpeechParts = excludedSpeechParts;
            //ExcludedSpeechParts = new[] {PartsOfSpeech.SpeechPart.V};
        }

        private bool CheckWord(string inputWord, out string wordContent, out PartsOfSpeech.SpeechPart enumElementOfCurrentType)
        {
            var wordAndPart = inputWord.Split(new []{' ', ',', '='}, 3);
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

        private bool IsAllowedPartsContains(PartsOfSpeech.SpeechPart enumElementOfCurrentType)
        {
            var allowedNames = ExcludedSpeechParts.ToHashSet();
            //Enum.GetNames(typeof(PartsOfSpeech.SpeechPart));
            //var enumElementOfCurrentType = (PartsOfSpeech.SpeechPart)Enum.Parse(typeof(PartsOfSpeech.SpeechPart), type);
            if (!allowedNames.Contains(enumElementOfCurrentType))
                return true;

            return false;
            //здесь можно проверить часть речи
            //return PartsOfSpeech.SpeechPart.S;
        }

        public IEnumerable<Word> GetAnalyzedWords(IEnumerable<string> words)
        {
            foreach (var word in words)
            {
                if (CheckWord(word, out string content, out PartsOfSpeech.SpeechPart type))
                {
                    if (IsAllowedPartsContains(type))
                        yield return new Word(content, type);
                }
                /*
                foreach (var colorName in Enum.GetNames(typeof(PartsOfSpeech.SpeechPart)))
                {
                    var a = colorName;

                }
                */
            }
        }
    }
}
