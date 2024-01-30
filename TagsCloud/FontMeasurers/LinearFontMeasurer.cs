using Microsoft.Extensions.DependencyInjection;
using TagsCloud.Contracts;
using TagsCloud.CustomAttributes;
using TagsCloud.Entities;

namespace TagsCloud.FontMeasurers;

[Injection(ServiceLifetime.Singleton)]
public class LinearFontMeasurer : IFontMeasurer
{
    public MeasurerType Type => MeasurerType.Linear;

    public int GetFontSize(int wordFrequency, int minFrequency, int maxFrequency, int minFontSize, int maxFontSize)
    {
        var fontSize = minFontSize + (float)wordFrequency / maxFrequency * (maxFontSize - minFontSize);
        return (int)fontSize;
    }
}