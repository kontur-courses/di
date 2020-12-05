using System;
using System.Linq;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.TextProcessing
{
    public class WordsFilter : IWordsFilter
    {
        private readonly ISpeechPartsParser _speechPartsParser;
        private readonly ITextProcessingSettings _textProcessingSettings;
        private readonly ISpeechPartsFilter _speechPartsFilter;

        public WordsFilter(ISpeechPartsParser speechPartsParser,
            ITextProcessingSettings textProcessingSettings,
            ISpeechPartsFilter speechPartsFilter)
        {
            _speechPartsParser = speechPartsParser;
            _textProcessingSettings = textProcessingSettings;
            _speechPartsFilter = speechPartsFilter;
        }

        public string[] GetInterestingWords(string text)
        {
            if (text == null)
                throw new ArgumentException("String must be not null");
            var speechPartsAndWords = _speechPartsParser.ParseToPartSpeechAndWords(text);
            var interestingSpeechParts = _speechPartsFilter
                .GetInterestingSpeechParts(speechPartsAndWords.Keys.ToArray())
                .ToHashSet();
            return speechPartsAndWords
                .Where(keyValuePair => interestingSpeechParts.Contains(keyValuePair.Key))
                .SelectMany(words => words.Value)
                .Where(word => !_textProcessingSettings.BoringWords.Contains(word))
                .ToArray();
            ;
        }
    }
}