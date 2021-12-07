using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.Preprocessing;
using TagsCloudContainer.Settings.Interfaces;

namespace TagsCloudContainer.Settings
{
    public class SpeechPartWordColorMapperSettings : ISpeechPartWordColorMapperSettings
    {
        public Dictionary<SpeechPart, Color> ColorMap { get; }
        public Color DefaultColor { get; }

        public SpeechPartWordColorMapperSettings(Dictionary<SpeechPart, Color> colorMap, Color defaultColor)
        {
            ColorMap = colorMap;
            DefaultColor = defaultColor;
        }
    }
}