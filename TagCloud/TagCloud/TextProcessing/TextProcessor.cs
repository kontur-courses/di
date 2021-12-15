using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace TagCloud.TextProcessing
{
    internal class TextProcessor : ITextProcessor
    {
        private const string UtilFileName = "mystem.exe";
        private const string TempPath = @"c:\temp\output.txt";
        private const string Arguments = "-nl -ig -d --format json";
        private readonly IFileProvider _textProvider;

        public TextProcessor(IFileProvider textProvider)
        {
            _textProvider = textProvider;
        }

        public IEnumerable<Dictionary<string, int>> GetWordsWithFrequency(ITextProcessingOptions options)
        {
            foreach (var filePath in options.FilesToProcess)
            {
                var myStemErrorOut = string.Empty;
                using (var process = ConfigureProcess(_textProvider.GetTxtFilePath(filePath)))
                {
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

                var myStemResults = ParseMyStemResult();
                File.Delete(TempPath);

                yield return myStemResults
                    .Where(r => !options.ExcludePartOfSpeech.Contains(r.PartOfSpeech)
                                || options.IncludeWords.Contains(r.Lemma))
                    .Select(r => r.Lemma)
                    .Where(w => !options.ExcludeWords.Contains(w))
                    .GroupBy(s => s)
                    .OrderByDescending(g => g.Count())
                    .Take(options.Amount)
                    .ToDictionary(g => g.Key, g => g.Count());
            }
        }

        private static IEnumerable<MyStemResult?> ParseMyStemResult()
        {
            return File.ReadAllText(TempPath)
                .Split("\n", StringSplitOptions.RemoveEmptyEntries)
                .Select(JsonConvert.DeserializeObject<MyStemResult>)
                .Where(r => r?.analysis.Count > 0);
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