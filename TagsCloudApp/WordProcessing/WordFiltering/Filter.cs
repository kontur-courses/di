using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using NHunspell;

namespace TagsCloudApp.WordFiltering
{
    public class Filter: IWordFilter
    {
        private readonly string[] unusedPartsOfSpeech = { "PR", "ADV", "CONJ", "PART", "SPRO" };

        public IEnumerable<string> FilterWords(IEnumerable<string> words)
        {
            words = words.Select(w => w.ToLower());
            var myStemOutput = GetMyStemOutput(words);
            var filteredWords = myStemOutput
                .Split('\n')
                .Where(s => s != "" && !unusedPartsOfSpeech.Any(p => s.Contains($"={p}")))
                .Select(s => s.Split('{')[0]);
            foreach (var word in filteredWords)
                yield return word;
        }

        private string GetMyStemOutput(IEnumerable<string> words)
        {
            File.WriteAllLines("temp.txt", words);

            var result = "";

            using (var myStemProcess = new Process())
            {
                myStemProcess.StartInfo.StandardOutputEncoding = Encoding.UTF8;
                myStemProcess.StartInfo.FileName = @"C:\Users\Nikita\source\repos\StudentThatW\di\TagsCloudApp\bin\Debug\Text\mystem.exe";
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
