using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.Preprocessing;
using TagsCloudContainer.Rendering;

namespace TagsCloudContainer.Settings
{
    public interface IWordsColorSettings
    {
        IWordColorMapper ColorMapper { get; set; }
    }

    public class DefaultWordsColorSettings : IWordsColorSettings
    {
        public IWordColorMapper ColorMapper { get; set; }

        public DefaultWordsColorSettings(IWordSpeechPartParser wordSpeechPartParser)
        {
            ColorMapper = new SpeechPartWordColorMapper(
                wordSpeechPartParser, new Dictionary<SpeechPart, Color>
                {
                    [SpeechPart.S] = Color.Crimson,
                    [SpeechPart.V] = Color.SlateBlue
                }, Color.Aqua);
        }
    }
}