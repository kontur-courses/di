using System.Collections.Generic;
using Newtonsoft.Json;

namespace TagCloud.TextProcessing
{
    internal class MyStemResultDto
    {
        [JsonProperty(PropertyName = "analysis")]
        public List<Dictionary<string, string>> Analysis { get; set; }

        [JsonProperty(PropertyName = "text")] public string Text { get; set; }
    }
}