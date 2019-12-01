namespace TagsCloudContainer.TextParsing.CloudParsing
{
    public class CloudWordsParserSettings
    {
        public IFileWordsParser FileWordsParser { get; set; }
        public string Path { get; set; }
        public ICloudWordParsingRule Rule { get; set; }
    }
}