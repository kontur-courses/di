using System.Collections.Generic;

namespace TagsCloudVisualization.WordProcessors
{
    public interface ITextProcessor
    {
        IEnumerable<string> ProcessWords(IEnumerable<string> text);
    }
}