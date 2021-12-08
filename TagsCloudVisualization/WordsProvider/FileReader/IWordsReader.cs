using System.Collections.Generic;

namespace TagsCloudVisualization.WordsProvider.FileReader
{
    public interface IWordsReader
    {
        bool CanRead(string extension);
        string Read(string filename);
    }
}