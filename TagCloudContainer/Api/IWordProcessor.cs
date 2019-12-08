using System.Collections.Generic;

namespace TagCloudContainer.Api
{
    public interface IWordProcessor
    {
        IEnumerable<string> Process(IEnumerable<string> words);
    }
}