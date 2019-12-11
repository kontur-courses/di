using System.Collections.Generic;

namespace TagsCloudContainer.Data.Processors
{
    public interface IWordProcessor
    {
        IEnumerable<string> Process(IEnumerable<string> words);
    }
}