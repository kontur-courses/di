namespace TagsCloudVisualization.Settings;

public class TextHandlerSettings
{
    public string PathToBoringWords { get; }
    public string PathToText { get; }

    public TextHandlerSettings(string pathToBoringWords, string pathToText)
    {
        PathToBoringWords = pathToBoringWords;
        PathToText = pathToText;
    }
}