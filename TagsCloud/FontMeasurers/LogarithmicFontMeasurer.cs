using Microsoft.Extensions.DependencyInjection;
using TagsCloud.Contracts;
using TagsCloud.CustomAttributes;
using TagsCloud.Entities;

namespace TagsCloud.FontMeasurers;

[Injection(ServiceLifetime.Singleton)]
public class LogarithmicFontMeasurer : IFontMeasurer
{
    public MeasurerType Type => MeasurerType.Logarithmic;

    public int GetFontSize(int wordFrequency, int minFrequency, int maxFrequency, int minFontSize, int maxFontSize)
    {
        var minLog = Math.Log(minFrequency);
        var divisor = Math.Log(maxFrequency) - minLog;
        var weight = divisor == 0 ? 1 : (Math.Log(wordFrequency) - minLog) / divisor;
        return (int)(minFontSize + (maxFontSize - minFontSize) * weight);
    }
}