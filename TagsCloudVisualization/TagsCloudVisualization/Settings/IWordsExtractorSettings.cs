using System.Collections.Generic;

namespace TagsCloudVisualization.Settings
{
    public interface IWordsExtractorSettings
    {
        List<char> StopChars { get; set; }

        List<string> StopWords { get; set; }
    }
}