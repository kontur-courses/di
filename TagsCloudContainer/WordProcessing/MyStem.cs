using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace TagsCloudContainer.WordProcessing
{
    public class MyStem : IWordNormalizer
    {
        private readonly string workingDirectory;

        public MyStem()
        {
            workingDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent?.Parent?.FullName + "\\MyStem";
        }

        public IEnumerable<string> NormalizeWords(IEnumerable<string> words)
        {
            var inputFile = $"{workingDirectory}\\input.txt";
            var inputFile1 = "input.txt";
            File.WriteAllLines(inputFile, words);
            var outputFile = $"{workingDirectory}\\output.txt";
            var outputFile1 = "output.txt";
            var arguments = $"/C mystem.exe {inputFile1} {outputFile1} -cls";
            var startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = arguments,
                WorkingDirectory = workingDirectory,
                UseShellExecute = true
            };
            var process = new Process {StartInfo = startInfo};
            process.Start();
            process.WaitForExit();

            return File.ReadLines(outputFile)
                .Select(Parse)
                .Select(s => s.ToLower());
        }

        private static string Parse(string line)
        {
            var match = Regex.Match(line, @"{([a-zA-Zа-яА-Я]+)(\?.*)?}").Groups;
            return match[1].Value;
        }
    }
}