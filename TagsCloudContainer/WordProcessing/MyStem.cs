using System;
using System.Diagnostics;
using System.IO;

namespace TagsCloudContainer.WordProcessing
{
    public class MyStem
    {
        public string WorkingDirectory => GetMyStemWorkingDirectory();
        
        public void ProcessWords(string inputFilePath, string outputFile)
        {
            var arguments = $"/C mystem.exe {inputFilePath} {outputFile} -cls";
            var workingDirectory = WorkingDirectory;
            var startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = arguments,
                WorkingDirectory = workingDirectory
            };
            var process = new Process {StartInfo = startInfo};
            process.Start();
            process.WaitForExit();
        }

        private static string GetMyStemWorkingDirectory()
        {
            var workingDirectory = Environment.CurrentDirectory;
            return Directory.GetParent(workingDirectory).Parent?.Parent?.FullName + "\\MyStem";
        }
    }
}