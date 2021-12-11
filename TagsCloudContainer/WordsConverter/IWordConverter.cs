using System.Collections.Generic;

namespace TagsCloudContainer.WordsConverter
{
    public interface IWordConverter
    {
        IEnumerable<Tag> ConvertWords(List<string> words);
    }
}