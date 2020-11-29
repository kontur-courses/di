using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace TagCloudCreator
{
    public class WordPrepairer
    {
        private static readonly List<string> BoringPOS = new List<string>()
            {"CONJ", "INTJ", "PART", "PR", "ADVPRO", "SPRO"};

        public static string[] GetInterestingWords(string[] words)
        {
            File.WriteAllLines("in.txt", words);
            var process = new Process();
            process.StartInfo.FileName = "mystem.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.Arguments = @"-i -l -n in.txt out.txt";
            process.Start();
            process.WaitForExit();
            var preparedWords = File.ReadAllLines("out.txt")
                .Where(line => !BoringPOS.Contains(line.Split(',')[0].Split('=')[1]))
                .Select(x => x.Split('=')[0].TrimEnd('?')).ToArray();
            File.Delete("in.txt");
            File.Delete("out.txt");
            return preparedWords;
        }

        public static List<(string, int)> GetWordsStatistic(IEnumerable<string> words)
        {
            var statistic = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (!statistic.ContainsKey(word)) statistic[word] = 0;
                statistic[word]++;
            }

            return statistic.Select(x => (x.Key, x.Value)).ToList();
        }
    }
}