using System.Collections.Generic;
using ResultProject;

namespace TagsCloudVisualization.WordProcessors
{
    internal interface ITextProcessor
    {
        Result<IEnumerable<string>> ProcessWords(IEnumerable<string> text);
    }
}