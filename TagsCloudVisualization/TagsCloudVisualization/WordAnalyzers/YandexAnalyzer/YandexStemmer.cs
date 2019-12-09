using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using TagsCloudVisualization.Structures;

namespace TagsCloudVisualization.WordAnalyzers.YandexAnalyzer
{
    public class YandexStemmer: IMorphAnalyzer
    {
        private readonly Regex regExForStandardWordForm = new Regex("\"lex\":\"(\\w+)\"", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private readonly Regex regExForPartOfSpeech = new Regex("\"gr\":\"(\\w+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);


        public string DefinePartOfSpeech(string toDefine) 
            => regExForPartOfSpeech.Match(toDefine).Groups[1].Value;

        public string GetStandardForm(string str) 
            => regExForStandardWordForm.Match(str).Groups[1].Value;
        
        public IEnumerable<WordInfo> AnalyzeText(string text)
        {
            using (var cmd = new Process())
            {
                cmd.StartInfo.FileName = "cmd.exe";
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.UseShellExecute = false;
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.CreateNoWindow = true;
                cmd.Start();
                cmd.StandardInput.WriteLine("cd YandexStem");
                cmd.StandardInput.WriteLine($"echo {text} | mystem.exe -e cp866 -nig --format json");
                var stemmedString = cmd.StandardOutput.ReadLine();
                /*
                 * В прошлой реализации была проблема, что шарп не дожидался завершения cmd скрипта и обработанный текст просто не успевал записаться в output.txt
                 * поэтому я щас считываю с stdoutput построчно, чтобы исключить возможность подобной ошибки.
                 * Пропускаю 6 строк т.к при запуске нового потока cmd в консоли выводятся строки типа: "Microsoft Windows [Version 10.0.18362.476]" + запуск стеммера
                 */
                var uselessLinesCounter = 0;
                var skippedLineCount = 6;
                while (stemmedString != string.Empty || uselessLinesCounter < skippedLineCount)
                {
                    stemmedString = cmd.StandardOutput.ReadLine();
                    uselessLinesCounter++;
                    yield return new WordInfo(GetStandardForm(stemmedString), DefinePartOfSpeech(stemmedString));
                }
            }
        }
    }
}
