using System.Collections.Generic;

namespace TagsCloudContainer.Preprocessing
{
    public interface IWordSpeechPartParser
    {
        IEnumerable<SpeechPartWord> ParseWords(IEnumerable<string> words);
    }
}