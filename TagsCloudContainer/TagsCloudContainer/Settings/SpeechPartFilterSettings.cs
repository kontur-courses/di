using System.Collections.Generic;
using TagsCloudContainer.Preprocessing;
using TagsCloudContainer.Settings.Interfaces;

namespace TagsCloudContainer.Settings
{
    public class SpeechPartFilterSettings : ISpeechPartFilterSettings
    {
        public SpeechPartFilterSettings(HashSet<SpeechPart> speechPartsToRemove)
        {
            SpeechPartsToRemove = speechPartsToRemove;
        }

        public HashSet<SpeechPart> SpeechPartsToRemove { get; }
    }
}