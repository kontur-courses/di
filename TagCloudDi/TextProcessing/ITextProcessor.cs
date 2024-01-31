namespace TagCloudDi.TextProcessing
{
    public interface ITextProcessor
    {
        public Dictionary<string, int> GetWordsFrequency();
    }
}
