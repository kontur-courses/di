using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.Preprocessing;

namespace TagsCloudContainer.Settings.Interfaces
{
    public interface ISpeechPartWordColorMapperSettings
    {
        Dictionary<SpeechPart, Color> ColorMap { get; }
        Color DefaultColor { get; }
    }
}