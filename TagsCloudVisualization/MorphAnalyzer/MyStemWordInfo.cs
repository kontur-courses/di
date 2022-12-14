using System.Text.Json.Serialization;

namespace TagsCloudVisualization.MorphAnalyzer;

public class MyStemWordInfo
{
    [JsonPropertyName("analysis")]
    public List<MyStemWordInfoAnalysis> Analysis { get; set; }
    [JsonPropertyName("text")]
    public string Text { get; set; }
}