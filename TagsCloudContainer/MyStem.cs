using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using TagsCloudContainer.FileManager;

namespace TagsCloudContainer
{
    public abstract class MyStem
    {
        protected readonly IFileManager FileManager;
        private readonly string pathToMyStem;

        protected MyStem(IFileManager fileManager, string pathToMyStem = null)
        {
            FileManager = fileManager;
            this.pathToMyStem = pathToMyStem;
        }

        protected void RunMyStem(string inputFilePath, string outputFilePath, string options)
        {
            var startupPath = Application.StartupPath;
            var myStemPath = pathToMyStem ?? Path.Combine(startupPath, "mystem.exe");

            if (!File.Exists(myStemPath))
                throw new FileNotFoundException($"mystem.exe not found in {startupPath}");

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

            const string errorMessage = "failed run mystem.exe";
            if (process == null)
                throw new Exception($"{errorMessage}. process is null");
            process.WaitForExit();
            if (process.ExitCode != 0)
                throw new Exception($"{errorMessage}. process exit with code {process.ExitCode}");
        }
        
    }
}