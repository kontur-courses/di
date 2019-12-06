using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using NHunspell;

namespace TagsCloudContainer
{
    public class SimpleWordsFilter : IWordsFilter
    {
        private readonly string[] Words;
        private HashSet<string> excludedTypes = new HashSet<string>() {
            "PR",
            "PART",
            "CONJ"
            };
        
        public SimpleWordsFilter(string[] arr)
        {
            Words = arr;
        }

        public IEnumerable<string> FilterWords()
        {
            //Hunspell hunspell = new Hunspell("en_us.aff", "en_us.dic");
             return Words.Select(x=>x.ToLower()).Where(x=> !IsBoring(GetInfoAboutWord(x)));
        }

        private bool IsBoring(string wordInfo)
        {
            var regexp = "(\\w)+=(\\w)\\s,";
            var matchRes = Regex.Match(wordInfo, regexp);
            if (matchRes.Success)
            {
                if(excludedTypes.Contains(matchRes.Groups[1].Value))
                    return  true;
            }

            return false;
        }

        private string GetInfoAboutWord(string word)
        {
            var outputBuilder = new StringBuilder();
            using (var process = new Process
            {
                StartInfo =
                {
                    FileName = "mystem.exe",
                    Arguments = "-lni",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true
                }
            })
            {
                process.OutputDataReceived += (sender, eventArgs) => outputBuilder.AppendLine(eventArgs.Data);
                process.Start();
                //var sw = new StreamWriter(process.StandardInput.BaseStream);
                  //  sw.WriteLine(word);
                process.StandardInput.WriteLine(word);
                //var sr =  new StreamReader(process.StandardOutput.BaseStream);
                   // var res = sr.ReadToEnd();
                  //  sw.Close();
                    //sr.Close();
                    process.WaitForExit();
                //return res;
            }

            return outputBuilder.ToString();
        }
    }

    
}