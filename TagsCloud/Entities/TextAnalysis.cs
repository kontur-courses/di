using System.Text.Json.Serialization;

namespace TagsCloud.Entities;

public class TextAnalysis
{
    [JsonPropertyName("analysis")] public List<AnalysisPart> AnalysisItems { get; set; }
    [JsonPropertyName("text")] public string Text { get; set; }

    public class AnalysisPart
    {
        [JsonPropertyName("lex")] public string Lexico { get; set; }
        [JsonPropertyName("gr")] public string Grammar { get; set; }
    }
}