using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using TagsCloudContainer.FileManager;
using TagsCloudContainer.Filters;

namespace TagsCloudContainer.TokensGenerator
{
    public class MyStem : ITokensParser, IFilter
    {
        private readonly IFileManager fileManager;
        private readonly WordType[] excludedWorldType;
        private readonly string pathToMyStem;

        public MyStem(IFileManager fileManager, string pathToMyStem = null)
        {
            this.fileManager = fileManager;
            this.pathToMyStem = pathToMyStem;
        }

        public MyStem(IFileManager fileManager, WordType[] excludedWorldType, string pathToMyStem = null)
        {
            this.fileManager = fileManager;
            this.excludedWorldType = excludedWorldType;
            this.pathToMyStem = pathToMyStem;
        }

        private void RunMyStem(string inputFilePath, string outputFilePath, string options)
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

            var errorMessage = "failed run mystem.exe";
            if (process == null)
                throw new Exception($"{errorMessage}. process is null");
            process.WaitForExit();
            if (process.ExitCode != 0)
                throw new Exception($"{errorMessage}. process exit with code {process.ExitCode}");
        }

        public IEnumerable<string> GetTokens(string str)
        {
            var inputFilePath = fileManager.MakeFile();
            var outputFilePath = fileManager.MakeFile();

            fileManager.WriteInFile(inputFilePath, str);

            RunMyStem(inputFilePath, outputFilePath, $"--format json -dn");


            var strFile = fileManager.ReadFile(outputFilePath);
            var res = strFile.Split(new[] {"\n", "\r\n"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => JObject.Parse(s)["analysis"].FirstOrDefault()?["lex"]);

            return res.Where(s => s != null).Select(s => s.ToString());
        }

        public IEnumerable<string> Filtering(IEnumerable<string> tokens)
        {
            var inputFilePath = fileManager.MakeFile();
            var outputFilePath = fileManager.MakeFile();

            fileManager.WriteInFile(inputFilePath, string.Join("\r\n", tokens));
            RunMyStem(inputFilePath, outputFilePath, $"--format json -dgin");


            var strFile = fileManager.ReadFile(outputFilePath);
            var res = strFile.Split(new[] {"\n", "\r\n"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(JObject.Parse)
                .Select(Token.FromJson)
                .Where(t => !excludedWorldType.Contains(t.WordType));

            return res.Select(t => t.Value).Where(s => s.Length > 3);
        }
    }
}