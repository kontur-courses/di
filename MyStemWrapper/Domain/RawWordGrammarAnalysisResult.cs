using System.Text.Json.Serialization;

namespace MyStemWrapper.Domain;

public class RawWordGrammarAnalysisResult
{
    [JsonPropertyName("lex")] public string? Lexeme { get; set; }

    [JsonPropertyName("gr")] public string? Grammar { get; set; }
}