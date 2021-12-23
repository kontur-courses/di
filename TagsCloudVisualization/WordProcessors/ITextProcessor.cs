using System.Collections.Generic;
using ResultProject;

namespace TagsCloudVisualization.WordProcessors
{
    public interface ITextProcessor
    {
        Result<IEnumerable<string>> ProcessWords(IEnumerable<string> text);
    }
}