namespace TagsCloudContainer.FrequencyAnalyzers
{
    public interface IAnalyzer
    {
        public void Analyze(string text, string exclude = "");
        public IEnumerable<(string, int)> GetAnalyzedText();
    }
}