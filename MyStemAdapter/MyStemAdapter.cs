using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MyStemAdapter
{
    public class MyStemAdapter
    {
        private readonly string myStemExePath;

        public MyStemAdapter(string myStemExePath = "mystem.exe")
        {
            this.myStemExePath =
                $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}{Path.DirectorySeparatorChar}{myStemExePath}";
        }

        public async Task<WordInfo> GetWordInfo(string word)
        {
            var myStemOutput = await LaunchMyStemAsync("-ngi --format json", word);
            var myStemAnalysis = JsonConvert.DeserializeObject<TextAnalysisResult>(myStemOutput);
            
            if (!myStemAnalysis.Analysis.Any()) 
                return new WordInfo(word, PartOfSpeech.Unknown, string.Empty);
            
            var wordAnalysis = myStemAnalysis.Analysis.First();
            return new WordInfo(word, wordAnalysis.PartOfSpeech, wordAnalysis.Lex);

        }

        private async Task<string> LaunchMyStemAsync(string argString, string input)
        {
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = myStemExePath,
                    Arguments = argString,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true,
                    CreateNoWindow = true,
                }
            };
            proc.Start();
            await proc.StandardInput.WriteAsync(proc.StandardInput.Encoding.GetString(Encoding.UTF8.GetBytes(input)));
            proc.StandardInput.Close();
            proc.WaitForExit();

            var line = await proc.StandardOutput.ReadToEndAsync();
            return Encoding.UTF8.GetString(proc.StandardOutput.CurrentEncoding.GetBytes(line));
        }
    }
}
