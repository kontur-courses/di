using System;
using TagCloud.FileReaders;
using TagCloud.Layouters;
using TagCloud.TextAnalyzer;

namespace TagCloud
{
    public class TagCloud
    {
        private IRectangleLayouter layouter;
        private IFileReader fileReader;
        private ITextAnalyzer textAnalyzer;
        
        public TagCloud(IRectangleLayouter layouter, IFileReader fileReader, ITextAnalyzer textAnalyzer)
        {
            this.layouter = layouter;
            this.fileReader = fileReader;
            this.textAnalyzer = textAnalyzer;
        }

        public void GetWords(string path)
        {
            var words = fileReader.ReadWords(path);
            foreach (var wordToCount in textAnalyzer.GetWordCounts(words))
            {
                Console.WriteLine($"{wordToCount.Key} - {wordToCount.Value}");
            }
        }
    }
}