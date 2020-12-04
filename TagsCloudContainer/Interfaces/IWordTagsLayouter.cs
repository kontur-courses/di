using System.Collections.Generic;
using TagsCloudContainer.TextProcessing;

namespace TagsCloudContainer.Interfaces
{
    public interface IWordTagsLayouter
    {
        IEnumerable<WordTag> GetWordTags(string text);
    }
}