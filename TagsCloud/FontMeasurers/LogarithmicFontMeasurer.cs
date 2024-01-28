using TagsCloud.Contracts;
using TagsCloud.Entities;

namespace TagsCloud.FontMeasurers;

public class LogarithmicFontMeasurer : IFontMeasurer
{
    public MeasurerType Type => MeasurerType.Logarithmic;

    public int GetFontSize(int count, int maxCount, int minCount, int maxSize, int minSize)
    {
        var minLog = Math.Log(minCount);
        var divisor = Math.Log(maxCount) - minLog;
        var weight = divisor == 0 ? 1 : (Math.Log(count) - minLog) / divisor;
        return (int)(minSize + (maxSize - minSize) * weight);
    }
}