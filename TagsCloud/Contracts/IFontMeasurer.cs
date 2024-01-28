using TagsCloud.Entities;

namespace TagsCloud.Contracts;

public interface IFontMeasurer
{
    MeasurerType Type { get; }
    int GetFontSize(int count, int maxCount, int minCount, int maxSize, int minSize);
}