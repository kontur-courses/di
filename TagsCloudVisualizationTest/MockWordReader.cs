using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.WordProcessors;

namespace TagsCloudVisualizationTest
{
    public class MockWordReader : ITextProcessor
    {
        public IEnumerable<string> ProcessWords(IEnumerable<string> text)
        {
            return text;
        }
    }
}