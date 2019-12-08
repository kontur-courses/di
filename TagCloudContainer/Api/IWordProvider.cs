using System.Collections.Generic;

namespace TagCloudContainer.Api
{
    public interface IWordProvider
    {
        IEnumerable<string> GetWords();
    }
}