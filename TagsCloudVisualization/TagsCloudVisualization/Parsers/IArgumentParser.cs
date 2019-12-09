using TagsCloudVisualization.Settings;
using TagsCloudVisualization.TextRenderers;
using TagsCloudVisualization.WordAnalyzers;

namespace TagsCloudVisualization.Parsers
{
    public interface IArgumentParser
    {
        CloudSettings CreateCloudSettings(string[] args);
        ImageSettings CreateImageSettings(string[] args, ITextRenderer textRendere);
        TextSettings CreateTextSettings(string[] args, IMorphAnalyzer analyzer);
    }
}
