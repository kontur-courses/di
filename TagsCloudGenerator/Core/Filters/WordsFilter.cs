using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace TagsCloudGenerator.Core.Filters
{
    public class WordsFilter : IWordsFilter
    {
        private readonly string[] unusedPartsOfSpeech = {"PR", "ADV", "CONJ", "PART"};

        public IEnumerable<string> GetFilteredWords(IEnumerable<string> words)
        {
            var myStemOutput = GetMyStemOutput(words);

            foreach (var stemResult in myStemOutput.Split('\n').Where(s => s != ""))
            {
                if (!unusedPartsOfSpeech.Any(p => stemResult.Contains($"={p}")))
                {
                    yield return stemResult.Split('{')[0];
                }
            }
        }

        private string GetMyStemOutput(IEnumerable<string> words)
        {
            File.WriteAllLines("temp.txt", words);

            var result = "";

            using (var myStemProcess = new Process())
            {
                myStemProcess.StartInfo.StandardOutputEncoding = Encoding.UTF8;
                myStemProcess.StartInfo.FileName = "mystem.exe";
                myStemProcess.StartInfo.Arguments = "-ni temp.txt";
                myStemProcess.StartInfo.CreateNoWindow = true;
                myStemProcess.StartInfo.UseShellExecute = false;
                myStemProcess.StartInfo.RedirectStandardInput = true;
                myStemProcess.StartInfo.RedirectStandardOutput = true;
                myStemProcess.Start();
                result += myStemProcess.StandardOutput.ReadToEnd();
            }

            File.Delete("temp.txt");

            return result;
        }
    }
}