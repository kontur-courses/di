using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace TagCloud
{
    public class TextProcessor
    {
        private readonly ITextProcessingSettings _settings;

        public TextProcessor(ITextProcessingSettings settings)
        {
            _settings = settings;
        }

        public Dictionary<string, int> GetInterestingWords(string filepath, int amount = 1000)
        {
            var process = new Process();
            process.StartInfo.FileName = "mystem.exe";
            process.StartInfo.UseShellExecute = false;

            var tempPath = @"c:\temp\output.txt";
            var arguements = "-nld";
            
            process.StartInfo.Arguments = $"{arguements} {filepath} {tempPath}";
            process.Start();
            using var reader = new StreamReader(tempPath);
            var result = reader.ReadToEnd();
            return result.Split("\n", StringSplitOptions.RemoveEmptyEntries)
                .GroupBy(s => s)
                .ToDictionary(g => g.Key, g => g.ToList().Count);
        }
    }
}