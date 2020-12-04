using System.Collections.Generic;

namespace TagsCloudContainer.Interfaces
{
    public interface ISpeechPartsParser
    {
        Dictionary<string, List<string>> ParseToPartSpeechAndWords(string text);
    }
}