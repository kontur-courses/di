using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.Preprocessing;

namespace TagsCloudContainer.Settings
{
    public class SpeechPartColorMapSettings : ISpeechPartColorMapSettings
    {
        public Dictionary<SpeechPart, Color> ColorMap { get; }

        public SpeechPartColorMapSettings(IRenderSettings settings)
        {
            ColorMap = settings.SpeechPartColorMap;
        }
    }
}