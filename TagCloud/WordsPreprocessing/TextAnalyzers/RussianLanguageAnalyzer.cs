using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TagCloud.WordsPreprocessing.WordsSelector;

namespace TagCloud.WordsPreprocessing.TextAnalyzers
{
    /// <summary>
    /// Приводит слова русского языка к начальной форме 
    /// </summary>
    public class RussianLanguageAnalyzer : SimpleAnalyzer, ITextAnalyzer
    {
        private readonly ProcessStartInfo processInfo;
        private WordSelector wordSelector;

        public new string AnalyzerName => "RussianLanguageAnalyzer";

        public RussianLanguageAnalyzer(WordSelector wordSelector)
        {
            this.wordSelector = wordSelector;

            processInfo = new ProcessStartInfo("cmd.exe", "/c mystem -n -l -e cp866 -i")
            {
                RedirectStandardInput = true, UseShellExecute = false, RedirectStandardOutput = true
            };
        }

        public new Word[] GetWords(IEnumerable<string> words, int count)
        {
            words = words as string[] ?? words.ToArray();

            words = words.Select(w =>
            {
                var p = Process.Start(processInfo);
                p.StandardInput.WriteLine(w);
                p.StandardInput.Close();
                return p.StandardOutput.ReadToEnd();
            });


            return words
                .Select(w => w.Split('|')[0])
                .GroupBy(w => w)
                .Select(w =>
                {
                    var result = ParseWord(w.Key);
                    result.Count = w.Count();
                    return result;
                })
                .Where(wordSelector.CanUseThisWord)
                .Take(count)
                .Select(w =>
                {
                    w.Frequency = (double)w.Count / words.Count();
                    return w;
                })
                .ToArray();
        }

        private Word ParseWord(string wordWithLexemes)
        {
            var lexemes = wordWithLexemes.Split('=')[1].Split(',');
            SpeechPart speechPart;
            switch (lexemes[0])
            {
                case "V":
                    speechPart = SpeechPart.Verb;
                    break;
                case "A":
                    speechPart = SpeechPart.Adjective;
                    break;
                default:
                    speechPart = SpeechPart.Noun;
                    break;
            }
            return new Word(wordWithLexemes.Split('=')[0], speechPart);
        }
    }
}
