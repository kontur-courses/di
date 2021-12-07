using System.Collections.Generic;
using TagsCloudContainer.Preprocessing;

namespace TagsCloudContainer.Settings.Interfaces
{
    public interface ISpeechPartFilterSettings
    {
        HashSet<SpeechPart> SpeechPartsToRemove { get; }
    }
}