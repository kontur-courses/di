namespace TagsCloudContainer.Defaults.MyStem;

public class WordStat
{
    public WordStat(string stem, SpeechPart speechPart)
    {
        Stem = stem;
        SpeechPart = speechPart;
    }

    public static WordStat FromMyStemJson(string wordStat)
    {
        return new("", 0);
    }

    public string Stem { get; }
    public SpeechPart SpeechPart { get; }
}