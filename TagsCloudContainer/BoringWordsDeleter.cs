using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TagsCloudContainer.UI;

namespace TagsCloudContainer
{
    public class BoringWordsDeleter
    {
        public static IEnumerable<string> DeleteBoringWords(IEnumerable<string> words, IUi settings)
        {
            var result = new List<string>();
            var ignorePartOfSpeech = settings.IncludePartOfSpeech.Contains(' ')
                ? settings.IncludePartOfSpeech.Trim().Split(' ').Select(str => str.Trim())
                : new [] { settings.IncludePartOfSpeech };

            var deletePartOfSpeech = settings.ExceptPartOfSpeech.Contains(' ')
                ? settings.ExceptPartOfSpeech.Trim().Split(' ').Select(str => str.Trim())
                : new [] { settings.ExceptPartOfSpeech };

            var partsOfSpeech = new List<string>
                { "A", "ADV", "ADVPRO", "ANUM", "APRO", "COM", "CONJ", "NUM", "PART", "PR", "S", "SPRO", "V" };
            var boringPartsOfSpeech = new[] { "CONJ", "INTJ", "PART", "PR", "SPRO", "ADVPRO" }
                .Concat(deletePartOfSpeech)
                .Where(partsOfSpeech => !ignorePartOfSpeech.Contains(partsOfSpeech));

            var myStemProcess = new Process
            {
                StartInfo =
                {
                    FileName = "mystem.exe",
                    Arguments = "-i -n -e cp866",
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true
                }
            };


            myStemProcess.Start();
            foreach (var word in words)
            {
                myStemProcess.StandardInput.Write($"{word}\n");
                var wordInfo = myStemProcess.StandardOutput.ReadLine();
                if (wordInfo is null)
                    continue;
                var matchingParsOfSpeech =
                    partsOfSpeech.Where(p => wordInfo.IndexOf(p, StringComparison.Ordinal) != -1);
                var partOfSpeech = matchingParsOfSpeech.Any()
                    ? matchingParsOfSpeech.OrderByDescending(p => p.Length).First()
                    : null;
                if (!boringPartsOfSpeech.Contains(partOfSpeech))
                    result.Add(word);
            }

            myStemProcess.StandardInput.Close();
            myStemProcess.WaitForExit();
            myStemProcess.Close();
            myStemProcess.Dispose();

            return result;
        }
    }
}