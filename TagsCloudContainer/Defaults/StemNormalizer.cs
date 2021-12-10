using TagsCloudContainer.Abstractions;

namespace TagsCloudContainer.Defaults;

public class StemNormalizer : IWordNormalizer
{
    private readonly MyStem.MyStem myStem;

    public StemNormalizer(MyStem.MyStem myStem)
    {
        this.myStem = myStem;
    }

    public string? Normalize(string word)
    {
        return myStem.AnalyzeWord(word)?.Stem;
    }
}
