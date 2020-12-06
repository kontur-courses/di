using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace MyStem.Wrapper
{
    /// <summary>
    /// Unparsed analysis result for word
    /// </summary>
    public class AnalysisResultRaw
    {
        [JsonPropertyName("text")] public string Text { get; set; }

        [JsonProperty("analysis", Required = Required.DisallowNull)]
        public AnalysisResultEntryRaw[] Entries { get; set; }

        public override string ToString()
        {
            var entriesStr = Entries == null
                ? "<null>"
                : $"[{string.Join(", ", Entries.Select(e => $"<{e}>"))}]";
            return $"{nameof(Text)}={Text}, {nameof(Entries)}={entriesStr}";
        }
    }
}