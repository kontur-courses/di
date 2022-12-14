using System.Text.Json.Serialization;

namespace TagsCloudVisualization.MorphAnalyzer;

public class MyStemWordInfoAnalysis
{
    [JsonPropertyName("lex")]
    public string Lex { get; set; }

    [JsonPropertyName("gr")]
    public string Gr { get; set; }
}