namespace TagsCloudContainer.FrequencyAnalyzers
{
    public interface IAnalyzer
    {
        public abstract void Analyze(string text);
        public abstract IEnumerable<(string, int)> GetAnalyzedText();
    }
}
