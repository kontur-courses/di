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

        private static readonly ProcessStartInfo myStemStartInfo = new()
        {
            FileName = "executables/mystem.exe",
            Arguments = "-n -i -l -e cp866",
            RedirectStandardInput = true,
            RedirectStandardOutput = true
        };

        public IEnumerable<SpeechPartWord> ParseWords(IEnumerable<string> words)
        {
            if (words == null)
                throw new ArgumentNullException(nameof(words));

            using var myStem = Process.Start(myStemStartInfo);
            if (myStem == null)
                throw new ApplicationException("Can't start mystem.");

            var wordsInfos = new List<SpeechPartWord>();
            foreach (var word in words.Where(word => !string.IsNullOrWhiteSpace(word)))
            {
                var wordInfoResult = TryGetWordInfo(myStem, word);
                if (!wordInfoResult.Success)
                    throw wordInfoResult.Exception!;

                var speechPartGroup = speechPartRegex.Match(wordInfoResult.Value!).Groups["SpeechPart"];
                if (!speechPartGroup.Success || !Enum.TryParse<SpeechPart>(speechPartGroup.Value, out var speechPart))
                    throw GenerateSpeechPartParseException(word);

                wordsInfos.Add(new SpeechPartWord(word, speechPart));
            }

            return wordsInfos;
        }

        private static Result<string> TryGetWordInfo(Process myStem, string word)
        {
            myStem.StandardInput.WriteLine(word);
            var readTask = myStem.StandardOutput.ReadLineAsync();
            var canProcessWord = readTask.Wait(450);
            if (!canProcessWord || readTask.Result == null)
                return new Result<string>(GenerateSpeechPartParseException(word));

            return new Result<string>(readTask.Result);
        }

        private static ApplicationException GenerateSpeechPartParseException(string word) =>
            new($"Can't get speech part of '{word}'.");
    }
}