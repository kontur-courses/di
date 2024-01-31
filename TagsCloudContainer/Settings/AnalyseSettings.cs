namespace TagsCloudContainer.Settings;

public class AnalyseSettings: IAnalyseSettings
{
    public string[] ValidSpeechParts { get; set; } = { "V", "S", "A", "ADV", "NUM" };
}