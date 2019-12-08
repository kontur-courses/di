using TagsCloudVisualization.Settings;
using TagsCloudVisualization.WordAnalyzers;

namespace TagsCloudVisualization.Parsers
{
    public interface IArgumentParser
    {
        CloudSettings CreateCloudSettings(string[] args);
        ImageSettings CreateImageSettings(string[] args);
        TextSettings CreateTextSettings(string[] args, IMorphAnalyzer analyzer);
    }
}
