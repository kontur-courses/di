using System.Text.Json.Serialization;

namespace MyStemWrapper.Domain;

public class RawWordAnalysisResult
{
    [JsonPropertyName("text")] public string Word { get; set; } = null!;

    [JsonPropertyName("analysis")] public RawWordGrammarAnalysisResult[]? Guesses { get; set; }
}