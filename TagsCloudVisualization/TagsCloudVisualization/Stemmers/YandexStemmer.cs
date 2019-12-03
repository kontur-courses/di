using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace TagsCloudVisualization.Stemmers
{
    class YandexStemmer: IStemmer
    {

        private readonly  Regex regExForStandardWordForm = new Regex("\"lex\":\"(\\w+)\"", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private readonly Regex regExForPartOfSpeech = new Regex("\"gr\":\"(\\w+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        public IEnumerable<(string value, string valueForFilter)> GetStemmedString()
        {
            var cmd = new Process
            {
                StartInfo = {FileName = "cmd.exe", RedirectStandardInput = true, UseShellExecute = false,
                    RedirectStandardOutput = true, StandardOutputEncoding = Encoding.UTF8, CreateNoWindow = true}
            };
            cmd.Start();
            cmd.StandardInput.WriteLine("cd YandexStem");
            cmd.StandardInput.WriteLine("mystem.exe input.txt -nig --format json");
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
                var standardForm = regExForStandardWordForm.Match(stemmedString).Groups[1].Value.ToLower();
                var partOfSpeech = regExForPartOfSpeech.Match(stemmedString).Groups[1].Value;
                uselessLinesCounter++;
                yield return (standardForm, partOfSpeech);
            }
        }
    }
}
