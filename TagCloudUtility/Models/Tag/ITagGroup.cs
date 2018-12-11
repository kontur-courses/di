namespace TagCloud.Utility.Models.Tag
{
    public interface ITagGroup
    {
        FrequencyGroup FrequencyGroup { get; }
        int FontSize { get; }

        bool Contains(double frequencyCoef);
    }
}