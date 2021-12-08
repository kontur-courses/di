using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace TagsCloudContainer.Preprocessing
{
    public class WordSpeechPartParser : IWordSpeechPartParser
    {
        private static readonly Regex speechPartRegex = new(@".*?=(?'SpeechPart'\w+)");
        private static readonly Regex validWordRegex = new(@"^[а-я-]+$", RegexOptions.IgnoreCase);

        private static readonly ProcessStartInfo myStemStartInfo = new()
        {
            FileName = "executables/mystem.exe",
            Arguments = "-n -i -l -e cp866",
            RedirectStandardInput = true,
            RedirectStandardOutput = true
        };

        public IEnumerable<SpeechPartWord> ParseWords(IEnumerable<string> words)
        {
            using var myStem = Process.Start(myStemStartInfo);
            if (myStem == null)
                throw new ApplicationException($"Can't start mystem. Path: {myStemStartInfo.FileName}.");

            var filteredWords = words.Where(word => validWordRegex.IsMatch(word));
            return ParseWords(filteredWords, myStem).ToList();
        }

        private static IEnumerable<SpeechPartWord> ParseWords(IEnumerable<string> words, Process myStem)
        {
            foreach (var word in words.Where(word => validWordRegex.IsMatch(word)))
            {
                var wordInfoResult = TryGetWordInfo(myStem, word);
                if (!wordInfoResult.Success)
                    throw GenerateSpeechPartParseException(word);

                var speechPartGroup = speechPartRegex.Match(wordInfoResult.Value).Groups["SpeechPart"];
                if (!speechPartGroup.Success || !Enum.TryParse<SpeechPart>(speechPartGroup.Value, out var speechPart))
                    throw GenerateSpeechPartParseException(word);

                yield return new SpeechPartWord(word, speechPart);
            }
        }

        private static Result<string> TryGetWordInfo(Process myStem, string word)
        {
            myStem.StandardInput.WriteLine(word);
            var readTask = myStem.StandardOutput.ReadLineAsync();
            var canProcessWord = readTask.Wait(450);
            if (!canProcessWord || readTask.Result == null)
                return new Result<string>(new Exception("Can't get result from mystem."));

            return new Result<string>(readTask.Result);
        }

        private static ApplicationException GenerateSpeechPartParseException(string word) =>
            new($"Can't get speech part of '{word}'.");
    }
}