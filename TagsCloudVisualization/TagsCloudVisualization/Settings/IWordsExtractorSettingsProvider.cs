using System.Collections.Generic;

namespace TagsCloudVisualization.Settings
{
    public interface IWordsExtractorSettingsProvider
    {
        List<char> StopChars { get; set; }

        List<string> StopWords { get; set; }
    }
}
