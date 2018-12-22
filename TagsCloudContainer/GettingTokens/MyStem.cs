using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using TagsCloudContainer.Infrastructure.FileManaging;

namespace TagsCloudContainer.GettingTokens
{
    public class MyStem
    {
        private readonly IFileManager fileManager;

        public MyStem(IFileManager fileManager) =>
            this.fileManager = fileManager;
        
        public string Analyze(string text)
        {
            var inputFilePath = fileManager.MakeFile();
            var outputFilePath = fileManager.MakeFile();
            
            fileManager.WriteTextInFile(inputFilePath, text);
            RunMyStem(inputFilePath, outputFilePath);

            return fileManager.ReadTextFromFile(outputFilePath);
        }

        private void RunMyStem(string inputFilePath, string outputFilePath)
        {
            var startupPath = Application.StartupPath;
            var pathToMyStem = Path.Combine(startupPath, "mystem.exe");
            
            if (!File.Exists(pathToMyStem))   
                throw new FileNotFoundException($"mystem.exe not found in {startupPath}");
            
            var options = $"--format json -ni";

            var process = new Process
            {
                StartInfo = new ProcessStartInfo("mystem.exe")
                {
                    WorkingDirectory = Application.StartupPath, CreateNoWindow = true,
                    UseShellExecute = false,
                    Arguments = $"{inputFilePath} {outputFilePath} {options}"
                }
            };
            
            process.Start();

            var errorMessage = "failed run mystem.exe";
           
            if (process == null)
                throw new Exception($"{errorMessage}. process is null");

            process.WaitForExit();

            if (process.ExitCode != 0)
                throw new Exception($"{errorMessage}. process exit with code {process.ExitCode}");
        }
    }
}