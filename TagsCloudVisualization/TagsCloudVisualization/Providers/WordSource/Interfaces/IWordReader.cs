using System.Collections.Generic;

namespace TagsCloudVisualization.WordSource.Interfaces
{
    internal interface IWordReader
    {
        IEnumerable<string> SplitByPunctuation(IEnumerable<string> lineSource);
    }
}