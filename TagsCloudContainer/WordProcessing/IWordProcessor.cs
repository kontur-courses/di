using System.Collections.Generic;

namespace TagsCloudContainer.WordProcessing
{
    public interface IWordProcessor
    {
        IEnumerable<string> ProcessWords(IEnumerable<string> words);
    }
}