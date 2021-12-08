using System.Collections.Generic;
using TagsCloudContainer.Preprocessing;
using TagsCloudContainer.Settings.Interfaces;

namespace TagsCloudContainer.Settings
{
    public class SpeechPartFilterSettings : ISpeechPartFilterSettings
    {
        public HashSet<SpeechPart> SpeechPartsToRemove { get; }

        public SpeechPartFilterSettings(HashSet<SpeechPart> speechPartsToRemove)
        {
            SpeechPartsToRemove = speechPartsToRemove;
        }
    }
}