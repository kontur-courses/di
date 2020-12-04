using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace TagCloudCreator
{
    public static class WordPreparer
    {
        private static readonly List<string> BoringPos = new List<string>
            {"CONJ", "INTJ", "PART", "PR", "ADVPRO", "SPRO"};

        public static string[] GetInterestingWords(string[] words)
        {
            File.WriteAllLines("in.txt", words);
            var process = new Process
            {
                StartInfo =
                {
                    FileName = "mystem.exe", UseShellExecute = false, Arguments = @"-i -l -n in.txt out.txt",
                    CreateNoWindow = true
                }
            };
            process.Start();
            process.WaitForExit();
            var correctWordsAnalysis = File.ReadAllLines("out.txt")
                .Where(line => !line.Contains("??") && !BoringPos.Contains(line.Split(',')[0].Split('=')[1]));
            var preparedWords = correctWordsAnalysis.Select(x => x.Split('=')[0].TrimEnd('?')).ToArray();
            File.Delete("in.txt");
            File.Delete("out.txt");
            return preparedWords;
        }

        public static List<WordStatistic> GetWordsStatistic(IEnumerable<string> words)
        {
            var statistic = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (!statistic.ContainsKey(word)) statistic[word] = 0;
                statistic[word]++;
            }

            return statistic.Select(x => new WordStatistic(x.Key, x.Value)).ToList();
        }
    }
}