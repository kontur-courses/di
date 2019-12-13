using System.Collections.Generic;

namespace TagsCloudVisualization.Text
{
    public interface ITextReader
    {
        IEnumerable<string> GetAllWords(string filepath);
    }
}