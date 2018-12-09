using System.Collections.Generic;

namespace TagsCloudVisualization.WordProcessing.FileHandlers
{
    public interface FileHandler
    {
        string PathToFile { get; }
        IEnumerable<string> ReadFile();
    }
}