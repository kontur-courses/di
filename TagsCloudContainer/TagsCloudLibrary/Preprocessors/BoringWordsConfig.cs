using System.Collections.Generic;
using TagsCloudLibrary.MyStem;

namespace TagsCloudLibrary.Preprocessors
{
    public class BoringWordsConfig
    {
        public readonly IEnumerable<Word.PartOfSpeech> PartOfSpeechWhitelist;

        public BoringWordsConfig(IEnumerable<Word.PartOfSpeech> partOfSpeechWhitelist)
        {
            PartOfSpeechWhitelist = partOfSpeechWhitelist;
        }
    }
}
