using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace TagCloud.TextProcessing
{
    internal class MyStemManager : IMorphologyAnalyzer
    {
        private const string UtilFileName = "mystem.exe";
        private const string TempPath = @"c:\temp\output.txt";
        private const string Arguments = "-nl -ig -d --format json";

        public IEnumerable<ILexeme?> GetLexemesFrom(string filePath)
        {
            RunMyStem(filePath);
            var myStemResults = ParseMyStemResult();
            File.Delete(TempPath);
            return myStemResults;
        }

        private static void RunMyStem(string filePath)
        {
            using var process = ConfigureProcess(filePath);
            var myStemErrorOut = string.Empty;
            process.ErrorDataReceived += (s, e) => myStemErrorOut += e.Data;
            try
            {
                process.Start();
            }
            catch (Exception e)
            {
                throw new ApplicationException("MyStem не запустился" +
                                               " проверьте наличие утилиты в корневом каталоге программы", e);
            }

            process.BeginErrorReadLine();
            process.WaitForExit();
            if (!string.IsNullOrEmpty(myStemErrorOut))
                throw new WarningException(
                    "MyStem отработал с ошибками, обработка текста прервана.\n" +
                    "Проверьте исходный текст на соответствие кодировке UTF-8.\n" +
                    $"Вывод MyStem: {myStemErrorOut}");
        }

        private static IEnumerable<MyStemResult?> ParseMyStemResult()
        {
            return File.ReadAllText(TempPath)
                .Split("\n", StringSplitOptions.RemoveEmptyEntries)
                .Select(JsonConvert.DeserializeObject<MyStemResultDto>)
                .Select(MyStemResult.FromDto)
                .Where(r => r.Analysis.Count > 0);
        }

        private static Process ConfigureProcess(string filepath)
        {
            var process = new Process();
            process.StartInfo.FileName = UtilFileName;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.Arguments = $"{Arguments} {filepath} {TempPath}";
            return process;
        }
    }
}