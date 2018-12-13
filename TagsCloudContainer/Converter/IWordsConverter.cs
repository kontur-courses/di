using System.Collections.Generic;

namespace TagsCloudContainer.Converter
{
    public interface IWordsConverter
    {
        IEnumerable<string> Convert(IEnumerable<string> words);
    }
}