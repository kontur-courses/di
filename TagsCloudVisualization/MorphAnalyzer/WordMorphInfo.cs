namespace TagsCloudVisualization.MorphAnalyzer;

public class WordMorphInfo
{
    public List<string> PartsOfSpeech { get; private set; }


    public WordMorphInfo()
    {
        PartsOfSpeech = new List<string>();
    }
}