using System.Collections.Generic;
using TagsCloudVisualization.WordProcessing;

namespace TagsCloudVisualization.InterfacesForSettings
{
    public interface IWordsSettings
    {
        string PathToFile { get; set; }
        WordAnalyzer WordAnalyzer { get; set; }
        HashSet<string> BoringWords { get; set; }
    }
}