using System;
using TagsCloudContainer.WordsParser;

namespace TagsCloudContainer
{
    public class TagCloudContainer: ITagCloudContainer
    {
        private IWordsAnalyzer wordsAnalyzer;
        
        public TagCloudContainer(IWordsAnalyzer wordsAnalyzer)
        {
            this.wordsAnalyzer = wordsAnalyzer;
        }

        public void MakeTagCloud()
        {
            foreach (var (key, value) in wordsAnalyzer.AnalyzeWords())
                Console.WriteLine($"{key}: {value}");
        }

        public void SaveTagCloud()
        {
            throw new System.NotImplementedException();
        }
    }
}