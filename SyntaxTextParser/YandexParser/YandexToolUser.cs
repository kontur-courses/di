using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using SyntaxTextParser.Architecture;

namespace SyntaxTextParser.YandexParser
{
    public class YandexToolUser : ExternalToolUser
    {
        private const string ConsoleArgs = "nidg";
        private readonly bool needDeleteTempFiles;
        private const string TempInputFile = "TempInput.txt";
        private const string TempOutputFile = "TempOutput.txt";

        public YandexToolUser(string filePath, string toolName, bool needDeleteTempFiles = true) : 
            base(filePath, toolName)
        {
            this.needDeleteTempFiles = needDeleteTempFiles;
        }

        public override IEnumerable<string> ParseTextInTool(string text)
        {
            File.WriteAllText(TempInputFile, text);

            var pathToTool = Path.Combine(FilePath, ToolName);
            var process = Process.Start(pathToTool, $"-{ConsoleArgs} {TempInputFile} {TempOutputFile}");

            if (process == null)
                throw new Exception($"{ToolName} not found");
            process.WaitForExit();

            var resultText = File.ReadAllLines(TempOutputFile);
            if (needDeleteTempFiles)
            {
                File.Delete(TempInputFile);
                File.Delete(TempOutputFile);
            }

            return resultText;
        }
    }
}