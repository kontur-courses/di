using System.Collections.Generic;

namespace TagsCloudVisualization.Text
{
    public interface ITextReader
    {
        HashSet<string> Formats { get; }

        IEnumerable<string> GetAllWords(string filepath);
    }
}