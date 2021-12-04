using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace TagsCloudContainer.Preprocessing
{
    public interface IWordSpeechPartParser
    {
        IEnumerable<(string Word, SpeechPart SpeechPart)> ParseWords(IEnumerable<string> words);
    }

    public class WordSpeechPartParser : IWordSpeechPartParser
    {
        private static readonly Regex speechPartRegex = new Regex(@".*?=(?'SpeechPart'\w+)");

        private static readonly ProcessStartInfo myStemStartInfo = new()
        {
            FileName = "executables/mystem.exe",
            Arguments = "-n -i -l -e cp866",
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
        };

        public IEnumerable<(string Word, SpeechPart SpeechPart)> ParseWords(IEnumerable<string> words)
        {
            if (words == null)
                throw new ArgumentNullException(nameof(words));

            using var myStem = Process.Start(myStemStartInfo);
            if (myStem == null)
                throw new ApplicationException("Can't start mystem.");

            var wordsInfos = new List<(string, SpeechPart)>();
            foreach (var word in words.Where(word => !string.IsNullOrWhiteSpace(word)))
            {
                if (!TryGetWordInfo(myStem, word, out var wordInfo))
                    throw GenerateSpeechPartParseException(word);

                var speechPartGroup = speechPartRegex.Match(wordInfo).Groups["SpeechPart"];
                if (!speechPartGroup.Success || !Enum.TryParse<SpeechPart>(speechPartGroup.Value, out var speechPart))
                    throw GenerateSpeechPartParseException(word);

                wordsInfos.Add((word, speechPart));
            }

            return wordsInfos;
        }

        private static bool TryGetWordInfo(Process myStem, string word, out string wordInfo)
        {
            wordInfo = null;

            myStem.StandardInput.WriteLine(word);
            var readTask = myStem.StandardOutput.ReadLineAsync();
            var canProcessWord = readTask.Wait(450);
            if (!canProcessWord)
                return false;

            wordInfo = readTask.Result;
            return wordInfo != null;
        }

        private static ApplicationException GenerateSpeechPartParseException(string word) =>
            new($"Can't get speech part of '{word}'.");
    }
}