using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System;

namespace TagsCloudContainer.WordPreprocessors
{
    public class SimpleWordPreprocessor : IWordPreprocessor
    {
        private Regex findInitialForm = new Regex(@"{(\w+)=");

        public string[] WordPreprocessing(string[] text)
        {
            return StemWords(text)
                .Select(word => word.ToLower())
                .ToArray();
        }

        private IEnumerable<string> StemWords(string[] text)
        {
            using (var cmd = new Process())
            {
                cmd.StartInfo.FileName = "cmd.exe";
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.UseShellExecute = false;
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.CreateNoWindow = true;
                cmd.Start();
                cmd.StandardInput.WriteLine("cd YandexStem");

                for (var i = 0; i < 5; i++)
                    cmd.StandardOutput.ReadLine();
                var stemWord = "def";
                
                foreach (var line in text)
                {
                    cmd.StandardInput.WriteLine($@"echo {line} | mystem.exe -e cp866 -nig");
                    cmd.StandardOutput.ReadLine();
                    stemWord = cmd.StandardOutput.ReadLine();
                    while (stemWord != "")
                    {
                        yield return findInitialForm.Match(stemWord).Groups[1].Value;
                        stemWord = cmd.StandardOutput.ReadLine();
                    }
                }
            }

        }
    }
}
