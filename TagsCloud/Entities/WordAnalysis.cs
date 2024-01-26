using System.Text.Json.Serialization;

namespace TagsCloud.Entities;

public class WordInfo
{
    [JsonPropertyName("analysis")] public List<WordAnalysis> Analyses    { get; set; }
    [JsonPropertyName("text")]     public string             InitialWord { get; set; }
    public                                bool               IsRussian   => Analyses.Count > 0;
}