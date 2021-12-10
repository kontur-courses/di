using System.Collections.Generic;

namespace TagsCloudVisualization.WordsProvider
{
    public interface IFileReadService
    {
        IEnumerable<string> GetFileContent();
    }
}