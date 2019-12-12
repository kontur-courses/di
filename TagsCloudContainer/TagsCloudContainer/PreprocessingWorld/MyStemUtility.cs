using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace TagsCloudContainer.PreprocessingWorld
{
    public class MyStemUtility : IPreprocessingWorld
    {
        private readonly string pathMyStemUtility;
        private readonly string flags;
        
        public MyStemUtility()
        {
            pathMyStemUtility = Environment.CurrentDirectory + @"\..\..\mystem.exe";
            flags = "-nig --format json";
        }

        private string WordProcessing(MyStemOutput myStemOutput)
        {
            if (myStemOutput.analysis != null)
                foreach (var wordInfo in myStemOutput.analysis)
                    if (wordInfo.gr.Contains("S") && wordInfo.gr.Contains("ед") && wordInfo.gr.Contains("им") )
                        return wordInfo.lex.ToLower();
            return null;
        }
        
        private IEnumerable<string> GetResult(string filePath)
        {
            var listWord = new List<string>();
            
            using (var proc = CreateProcess(filePath))
            {
                proc.Start();
                while (!proc.StandardOutput.EndOfStream)
                {
                    var word = WordProcessing(JsonConvert.DeserializeObject<MyStemOutput>(proc.StandardOutput.ReadLine()));
                    if (word != null)
                        listWord.Add(word);
                }
            }

            return listWord;
        }
        
        private Process CreateProcess(string filePath)
        {
            return new Process
            {
                StartInfo =
                {
                    FileName = pathMyStemUtility,
                    Arguments = flags + " " + filePath,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    StandardOutputEncoding = Encoding.UTF8
                }
            };
        }
        
        public IEnumerable<string> Preprocessing(IEnumerable<string> strings)
        {
            var pathTempFile = Path.GetTempFileName();
            using (var sw = File.CreateText(pathTempFile))
                foreach (var str in strings)
                    sw.WriteLine(str);

            var result = GetResult(pathTempFile);
            File.Delete(pathTempFile);
            return result;
        }
    }
}