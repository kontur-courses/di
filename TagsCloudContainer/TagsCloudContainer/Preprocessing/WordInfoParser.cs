using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace TagsCloudContainer.Preprocessing
{
    public class WordInfoParser : IWordInfoParser
    {
        private static readonly Regex speechPartRegex = new Regex(@".*?=(?'SpeechPart'\w+)");

        private static readonly ProcessStartInfo myStemStartInfo = new()
        {
            FileName = "executables/mystem.exe",
            Arguments = "-n -i -l -e cp866",
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
        };

        public IEnumerable<WordInfo> ParseWords(IEnumerable<string> words)
        {
            var wordsInfos = new List<WordInfo>();
            using var myStem = Process.Start(myStemStartInfo);
            if (myStem == null)
                throw new Exception("Can't start mystem");

            foreach (var word in words.Where(word => !string.IsNullOrWhiteSpace(word)))
            {
                if (!TryGetWordInfo(myStem, word, out var wordInfo))
                    throw GenerateSpeechPartParseException(word);

                var speechPartGroup = speechPartRegex.Match(wordInfo).Groups["SpeechPart"];
                if (!speechPartGroup.Success || !Enum.TryParse<SpeechPart>(speechPartGroup.Value, out var speechPart))
                    throw GenerateSpeechPartParseException(word);

                wordsInfos.Add(new WordInfo(word, speechPart));
            }

            return wordsInfos;
        }

        private bool TryGetWordInfo(Process myStem, string word, out string wordInfo)
        {
            wordInfo = null;

            myStem.StandardInput.WriteLine(word);
            var readTask = myStem.StandardOutput.ReadLineAsync();
            var canProcessWord = readTask.Wait(300);
            if (!canProcessWord)
                return false;

            wordInfo = readTask.Result;
            return wordInfo != null;
        }

        private static ApplicationException GenerateSpeechPartParseException(string word) =>
            new($"Can't get speech part of '{word}'");
    }
}