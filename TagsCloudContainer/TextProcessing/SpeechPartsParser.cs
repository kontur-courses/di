using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.TextProcessing
{
    public class SpeechPartsParser : ISpeechPartsParser
    {
        private readonly ITextConverter _textConverter;

        public SpeechPartsParser(ITextConverter textConverter)
        {
            _textConverter = textConverter;
        }

        public Dictionary<string, List<string>> ParseToPartSpeechAndWords(string text)
        {
            if (text == null)
                throw new ArgumentException("String must be not null");
            var myStemText = _textConverter.ConvertTextToCertainFormat(text);
            var partOfSpeechAndWords = new Dictionary<string, List<string>>();
            var regex = new Regex(@"\w+{(\w+=\w+).*}");
            foreach (var line in myStemText.Split(Environment.NewLine))
            {
                var match = regex.Match(line);
                if (!match.Success) continue;
                var wordAndPartOfSpeech = match.Groups[1].Value.Split('=');
                if (partOfSpeechAndWords.ContainsKey(wordAndPartOfSpeech[1]))
                    partOfSpeechAndWords[wordAndPartOfSpeech[1]].Add(wordAndPartOfSpeech[0]);
                else
                    partOfSpeechAndWords[wordAndPartOfSpeech[1]] = new List<string> {wordAndPartOfSpeech[0]};
            }

            return partOfSpeechAndWords;
        }
    }
}