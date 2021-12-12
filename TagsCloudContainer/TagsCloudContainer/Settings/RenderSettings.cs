using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.ColorMappers;
using TagsCloudContainer.MathFunctions;
using TagsCloudContainer.Preprocessing;

namespace TagsCloudContainer.Settings
{
    public class RenderSettings : IRenderSettings
    {
        public FontFamily FontFamily { get; }
        public int MaxFontSize { get; }
        public int MinFontSize { get; }
        public Size? ImageSize { get; }
        public float ImageScale { get; }
        public Color BackgroundColor { get; }
        public Color DefaultColor { get; }
        public WordColorMapperType ColorMapperType { get; }
        public Dictionary<SpeechPart, Color> SpeechPartColorMap { get; }
        public MathFunctionType WordsScale { get; }
        public HashSet<SpeechPart> IgnoredSpeechParts { get; }

        public RenderSettings(
            FontFamily fontFamily,
            int maxFontSize,
            int minFontSize,
            Size? imageSize,
            float imageScale,
            Color backgroundColor,
            Color defaultColor,
            WordColorMapperType colorMapperType,
            Dictionary<SpeechPart, Color> speechPartColorMap,
            MathFunctionType wordsScale,
            HashSet<SpeechPart> ignoredSpeechParts)
        {
            FontFamily = fontFamily;
            MaxFontSize = maxFontSize;
            MinFontSize = minFontSize;
            ImageSize = imageSize;
            ImageScale = imageScale;
            BackgroundColor = backgroundColor;
            DefaultColor = defaultColor;
            ColorMapperType = colorMapperType;
            SpeechPartColorMap = speechPartColorMap;
            WordsScale = wordsScale;
            IgnoredSpeechParts = ignoredSpeechParts;
        }
    }
}