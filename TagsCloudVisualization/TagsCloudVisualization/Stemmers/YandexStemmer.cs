using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TagsCloudVisualization.Stemmers
{
    class YandexStemmer: IStemmer
    {
        public IEnumerable<string> GetStemmedString()
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
                uselessLinesCounter++;
                yield return stemmedString;
            }
        }
    }
}
