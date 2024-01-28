using System.Text.Json.Serialization;

namespace TagsCloud.Entities;

public class WordAnalysis
{
    private readonly char[] grammarSeparators = { ',', '=' };

    [JsonPropertyName("lex")]
    public string Infinitive { get; }

    [JsonPropertyName("gr")]
    public string Grammar { get; }

    public string LanguagePart => Grammar.Split(grammarSeparators)[0];
}