using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.TextProcessing
{
    public class WordsFilter : IWordsFilter
    {
        private readonly ISpeechPartsParser _speechPartsParser;
        private readonly ITextProcessingSettings _textProcessingSettings;

        private static readonly HashSet<string> _boringPartOfSpeech = new HashSet<string>
            {"PR", "PART", "INTJ", "CONJ", "ADVPRO", "APRO", "NUM", "SPRO"};

        public WordsFilter(ISpeechPartsParser speechPartsParser, ITextProcessingSettings textProcessingSettings)
        {
            _speechPartsParser = speechPartsParser;
            _textProcessingSettings = textProcessingSettings;
        }

        public string[] GetInterestingWords(string text)
        {
            if (text == null)
                throw new ArgumentException("String must be not null");
            var interestingWords = _speechPartsParser.ParseToPartSpeechAndWords(text)
                .Where(keyValuePair => !_boringPartOfSpeech.Contains(keyValuePair.Key))
                .Select(keyValue => keyValue.Value)
                .SelectMany(words => words)
                .Where(word => !_textProcessingSettings.BoringWords.Contains(word))
                .ToArray();
            return interestingWords;
        }
    }
}