namespace TagsCloudContainer.Core.WordsParser.Interfaces
{
    public interface IWordsAnalyzer
    {
        public Dictionary<string, int> AnalyzeWords();
    }
}
