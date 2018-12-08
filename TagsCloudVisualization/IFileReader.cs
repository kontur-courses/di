using System.Collections.Generic;


namespace TagsCloudVisualization
{
    public interface IFileReader
    {
        IEnumerable<string> ReadAllWords(string fileName);
    }
}
