using System.Collections.Generic;

namespace TagsCloudContainer.WordsConverter
{
    public interface IWordConverter
    {
        IEnumerable<Tag> ConvertWords(IEnumerable<string> words);
    }
}