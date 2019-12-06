using System.Collections.Generic;

namespace TagsCloudVisualization.Interfaces
{
    public interface ITextParser
    {
        IEnumerable<string> wordSource { get; }
    }
}