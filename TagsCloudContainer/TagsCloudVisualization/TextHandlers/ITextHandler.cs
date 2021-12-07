using System.Collections.Generic;

namespace TagsCloudVisualization.TextHandlers
{
    public interface ITextHandler
    {
        public IEnumerable<string> Handle(string filePath);
    }
}