namespace TagsCloudContainer.Defaults.MyStem;

public class WordStat
{
    public WordStat(string stem, SpeechPart speechPart)
    {
        Stem = stem;
        SpeechPart = speechPart;
    }

    public string Stem { get; }
    public SpeechPart SpeechPart { get; }
}