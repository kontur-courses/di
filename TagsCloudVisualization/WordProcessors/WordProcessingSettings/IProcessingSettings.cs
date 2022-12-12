namespace TagsCloudVisualization.WordProcessors.WordProcessingSettings
{
    public interface IProcessingSettings
    {
        private const int defaultMinWordLength = 2;
        private const int defaultMaxWordLength = 30;
        public int MinWordLength { get; set; }
        public int MaxWordLength { get; set; }
        public string[] ExcludedWords { get; set; }
    }
}
