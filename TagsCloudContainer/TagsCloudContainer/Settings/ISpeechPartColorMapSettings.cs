using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.Preprocessing;

namespace TagsCloudContainer.Settings
{
    public interface ISpeechPartColorMapSettings
    {
        Dictionary<SpeechPart, Color> ColorMap { get; }
    }
}