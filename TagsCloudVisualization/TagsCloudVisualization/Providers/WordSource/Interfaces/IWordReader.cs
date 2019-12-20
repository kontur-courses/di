using System.Collections.Generic;

namespace TagsCloudVisualization.Providers.WordSource.Interfaces
{
    internal interface IWordReader
    {
        IEnumerable<string> SplitByPunctuation(IEnumerable<string> lineSource);
    }
}