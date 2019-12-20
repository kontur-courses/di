using Newtonsoft.Json;

namespace TagsCloudContainer.PreprocessingWords
{
    internal class WordInfo
    {
        [JsonProperty("lex")] 
        public string LexicalForm { get; set; }

        [JsonProperty("gr")] 
        public string Grammar { get; set; }
    }
}