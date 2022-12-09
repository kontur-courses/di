namespace TagCloud.Parser.ParsingConfig.MyStemParsingConfig;

public class BoringWordsMyStemParsingConfig : MyStemParsingConfig
{
    private static readonly HashSet<string> ExcludedTypes = new HashSet<string>
    {
        "ADV",
        "ADVPRO",
        "APRO",
        "CONJ",
        "INTJ",
        "PART",
        "PR",
        "SPRO"
    };

    public override bool IsWordExcluded(string word)
    {
        return ExcludedTypes.Contains(GetWordType(word));
    }
}