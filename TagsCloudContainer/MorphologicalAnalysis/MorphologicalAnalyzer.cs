using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TagsCloudContainer.MorphologicalAnalysis
{
    public class MorphologicalAnalyzer
    {
        private const string ExecutedFile = "mystem.exe";
        private const string Options = "-nli";
        private readonly string executedFilePath;
        private readonly string inputPath;
        private readonly Process process;

        public MorphologicalAnalyzer(string filename)
        {
            executedFilePath = Directory.GetFiles(
                Directory.GetParent(Program.ProjectPath).FullName,
                ExecutedFile, SearchOption.AllDirectories)[0];
            inputPath = filename;
            process = InitProcess();
        }

        public IEnumerable<Word> GetWords()
        {
            foreach (var line in GetLines())
            {
                const string pattern = @"^([а-я-]*)\??=([A-Z]*)";
                var regex = new Regex(pattern);
                var match = regex.Match(line);

                var word = match.Groups[1].Value;
                var aliasPartSpeech = match.Groups[2].Value;
                var partSpeech = IdentifyPartSpeech(aliasPartSpeech);

                yield return new Word(word, partSpeech);
            }
        }

        private static PartSpeech IdentifyPartSpeech(string alias)
        {
            return alias switch
            {
                "A" => PartSpeech.Adjective,
                "ADV" => PartSpeech.Adverb,
                "ADVPRO" => PartSpeech.PronominalAdverb,
                "ANUM" => PartSpeech.NumeralAdjective,
                "APRO" => PartSpeech.PronounAdjective,
                "COM" => PartSpeech.PartComposite,
                "CONJ" => PartSpeech.Union,
                "INTJ" => PartSpeech.Interjection,
                "NUM" => PartSpeech.Numeral,
                "PART" => PartSpeech.Particle,
                "PR" => PartSpeech.Preposition,
                "S" => PartSpeech.Noun,
                "SPRO" => PartSpeech.PronounNoun,
                "V" => PartSpeech.Verb,
                _ => throw new NotImplementedException()
            };
        }

        public static PartSpeech GetPartSpeech(IEnumerable<string> partSpeeches)
        {
            return partSpeeches
                .Select(x => x.ToUpper())
                .Aggregate(PartSpeech.None,
                    (acc, next) => acc |= IdentifyPartSpeech(next));
        }

        private IEnumerable<string> GetLines()
        {
            process.Start();
            using (var reader = process.StandardOutput)
            {
                while (!reader.EndOfStream) yield return reader.ReadLine();
            }

            process.WaitForExit();
        }

        private Process InitProcess()
        {
            var arguments = Options + ' ' + Path.Combine(Program.ProjectPath, inputPath);

            var process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.StandardInputEncoding = Encoding.UTF8;
            process.StartInfo.StandardOutputEncoding = Encoding.UTF8;
            process.StartInfo.FileName = executedFilePath;
            process.StartInfo.Arguments = arguments;
            return process;
        }
    }
}