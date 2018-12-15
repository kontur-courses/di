using System.Collections.Generic;

namespace TagsCloudContainer.Filtering
{
    public interface IBoringWordsRepository
    {
        IEnumerable<string> Words { get; }
    }
}