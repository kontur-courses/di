using System.Collections.Generic;
using TagsCloudContainer.Preprocessing;

namespace TagsCloudContainer.Settings
{
    public interface ISpeechPartFilterSettings
    {
        HashSet<SpeechPart> SpeechPartsToRemove { get; }
    }
}