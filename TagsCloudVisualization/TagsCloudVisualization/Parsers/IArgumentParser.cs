using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Parsers
{
    interface IArgumentParser
    {
        CloudSettings CreateCloudSettings(string[] args);
        ImageSettings CreateImageSettings(string[] args);
        TextSettings CreateTextSettings(string[] args);
    }
}
