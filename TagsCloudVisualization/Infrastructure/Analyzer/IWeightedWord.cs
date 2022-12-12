namespace TagsCloudVisualization.Infrastructure.Analyzer
{
    public interface IWeightedWord
    {
        public int Weight { get; }

        public string Word { get; }
    }
}