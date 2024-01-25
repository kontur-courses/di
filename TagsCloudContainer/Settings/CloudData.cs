namespace TagsCloudContainer.Settings;

public class CloudData
{
    public string FilePath { get; set; }

    public Dictionary<string, int> WordsFrequency { get; set; } = new();
}