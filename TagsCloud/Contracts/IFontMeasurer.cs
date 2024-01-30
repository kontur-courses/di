using TagsCloud.Entities;

namespace TagsCloud.Contracts;

public interface IFontMeasurer
{
    MeasurerType Type { get; }
    int GetFontSize(int wordFrequency, int minFrequency, int maxFrequency, int minFontSize, int maxFontSize);
}