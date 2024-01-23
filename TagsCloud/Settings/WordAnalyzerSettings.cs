namespace TagsCloud;

public class WordAnalyzerSettings
{
    public List<string> BoringWords { get; set; } = new(); 

    public List<PartSpeech> ExcludedSpeeches { get; set; } = new()
    {
        PartSpeech.Interjection, PartSpeech.Preposition,
    };

    public List<PartSpeech> SelectedSpeeches { get; set; } = new();
}