using System.Collections.Generic;

namespace TagsCloudContainer.WordProcessing.Filtering.PartsOfSpeechQualifying
{
    public interface IPartOfSpeechQualifier
    {
        IEnumerable<(string, PartOfSpeech)> QualifyPartsOfSpeech(IEnumerable<string> words);
    }
}