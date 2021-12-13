using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace TagsCloudVisualizationDI.TextAnalization.Analyzer
{
    public interface IAnalyzer
    {
        public string FilePath { get; }
        public string MystemArgs { get; }
        public string SaveAnalizationPath { get; }
        public string MystemPath { get; }

        IEnumerable<Word> GetAnalyzedWords(IEnumerable<string> words);

        public void InvokeMystemAnalization()
        {
            if (!File.Exists(SaveAnalizationPath))
                throw new FileNotFoundException($"Giving path to file: {SaveAnalizationPath} is not valid, EXC");
            if (!File.Exists(MystemPath))
                throw new FileNotFoundException($"Giving path to mystemFile: {MystemPath} is not valid, EXC");

            var process = Process.Start(new ProcessStartInfo
            {
                FileName = MystemPath,
                Arguments = MystemArgs + ' ' + FilePath + ' ' + SaveAnalizationPath,
            });
            process.WaitForExit();
        }
    }
}
