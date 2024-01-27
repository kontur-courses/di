using System.Text.Json.Serialization;

namespace TagsCloud.Entities;

public class WordSummary
{
    [JsonPropertyName("analysis")]
    public List<WordAnalysis> Analyses { get; set; }
}