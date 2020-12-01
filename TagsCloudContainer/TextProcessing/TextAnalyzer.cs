using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MyStemWrapper;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.TextProcessing
{
    public class TextAnalyzer : ITextAnalyzer
    {
        private static readonly HashSet<string> _boringPartOfSpeech = new HashSet<string>
            {"PR", "PART", "INTJ", "CONJ", "ADVPRO", "APRO", "NUM", "SPRO"};

        public string[] GetInterestingWords(string text, string[] customBoringWords)
        {
            var hashSetBoringWords = customBoringWords.ToHashSet();
            if (string.IsNullOrEmpty(text))
                throw new ArgumentException("String must be not null and not empty");
            var mystemText = GetMyStemText(text);
            var interestingWords = Parser.ParseToPartSpeechAndWords(mystemText)
                .Where(keyValuePair => !_boringPartOfSpeech.Contains(keyValuePair.Key))
                .Select(keyValue => keyValue.Value)
                .SelectMany(words => words)
                .Where(word => !hashSetBoringWords.Contains(word))
                .ToArray();
            if (interestingWords.Length == 0)
                throw new ArgumentException("Text doesn't contain interesting words");
            return interestingWords;
        }

        private string GetMyStemText(string text)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var pathToMystem =
                Path.Combine(
                    currentDirectory.Substring(0,
                        currentDirectory.IndexOf("TagsCloudContainer", StringComparison.Ordinal) - 1), "mystem.exe");
            var mystem = new MyStem
            {
                PathToMyStem = pathToMystem,
                Parameters = "-ni"
            };
            return mystem.Analysis(text);
        }
    }
}