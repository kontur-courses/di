using System.Text.Json.Serialization;

namespace TagsCloud.Entities;

public class WordAnalysis
{
    private readonly char[] grammarSeparators = { ',', '=' };

    [JsonPropertyName("lex")]
    public string Infinitive { get; set; }

    [JsonPropertyName("gr")]
    public string Grammar { get; set; }

    public string LanguagePart => Grammar.Split(grammarSeparators)[0];
}