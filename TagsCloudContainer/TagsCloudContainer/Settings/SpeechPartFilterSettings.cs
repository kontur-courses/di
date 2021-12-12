using System.Collections.Generic;
using TagsCloudContainer.Preprocessing;

namespace TagsCloudContainer.Settings
{
    public class SpeechPartFilterSettings : ISpeechPartFilterSettings
    {
        public HashSet<SpeechPart> SpeechPartsToRemove { get; }

        public SpeechPartFilterSettings(IRenderSettings settings)
        {
            SpeechPartsToRemove = settings.IgnoredSpeechParts;
        }
    }
}