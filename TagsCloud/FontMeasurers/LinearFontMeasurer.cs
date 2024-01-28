using TagsCloud.Contracts;
using TagsCloud.Entities;

namespace TagsCloud.FontMeasurers;

public class LinearFontMeasurer : IFontMeasurer
{
    public MeasurerType Type => MeasurerType.Linear;

    public int GetFontSize(int count, int maxCount, int minCount, int maxSize, int minSize)
    {
        var fontSize = minSize + (float)count / maxCount * (maxSize - minSize);
        return (int)fontSize;
    }
}