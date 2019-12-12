using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using TagCloud.WordsPreprocessing.TextAnalyzers;
using TagCloud.WordsPreprocessing.WordsSelector;

namespace TagCloud.Interfaces
{
    public class ApplicationSettings
    {
        [Browsable(false)] public string FilePath { get; set; }

        [Browsable(false)] public Encoding TextEncoding { get; set; } = Encoding.Default;

        public StreamReader GetDocumentStream()
        {
            if (FilePath is null)
                throw new ArgumentNullException("Не задан файл");
            return new StreamReader(File.OpenRead(FilePath), TextEncoding);
        }

        [Browsable(false)] public ITextAnalyzer[] TextAnalyzers { get; }

        [Browsable(false)] public ITextAnalyzer CurrentTextAnalyzer { get; set; }


        public ApplicationSettings(ITextAnalyzer[] analyzers)
        {
            TextAnalyzers = analyzers;
            CurrentTextAnalyzer = TextAnalyzers.FirstOrDefault();
        }

        public ITextAnalyzer SetAnalyzer(string analyzer)
        {
            CurrentTextAnalyzer = TextAnalyzers.FirstOrDefault(w => w.AnalyzerName == analyzer);
            return CurrentTextAnalyzer;
        }
    }
}
