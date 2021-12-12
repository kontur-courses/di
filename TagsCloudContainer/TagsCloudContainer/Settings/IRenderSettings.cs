using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.ColorMappers;
using TagsCloudContainer.MathFunctions;
using TagsCloudContainer.Preprocessing;

namespace TagsCloudContainer.Settings
{
    public interface IRenderSettings
    {
        FontFamily FontFamily { get; }
        int MaxFontSize { get; }
        int MinFontSize { get; }
        Size? ImageSize { get; }
        float ImageScale { get; }
        Color BackgroundColor { get; }
        Color DefaultColor { get; }
        WordColorMapperType ColorMapperType { get; }
        Dictionary<SpeechPart, Color> SpeechPartColorMap { get; }
        MathFunctionType WordsScale { get; }
        HashSet<SpeechPart> IgnoredSpeechParts { get; }
    }
}