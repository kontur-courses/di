using System.Collections.Generic;

namespace TagsCloudVisualization.WordSource.Interfaces
{
    internal interface IObjectReader<T>
    {
        IEnumerable<T> SplitByPunctuation(IEnumerable<string> lineSource);
    }
}