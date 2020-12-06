using Newtonsoft.Json;

namespace MyStem.Wrapper
{
    /// <summary>
    /// Unparsed analysis result entry
    /// </summary>
    public class AnalysisResultEntryRaw
    {
        [JsonProperty("lex", Required = Required.AllowNull)]
        public string Lexeme { get; set; }

        [JsonProperty("gr", Required = Required.AllowNull)]
        public string Grammar { get; set; }

        public override string ToString() => $"{nameof(Lexeme)}={Lexeme ?? "<null>"}, " +
                                             $"{nameof(Grammar)}={Grammar ?? "<null>"}";
    }
}