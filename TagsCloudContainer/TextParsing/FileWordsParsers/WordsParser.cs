namespace TagsCloudContainer.TextParsing.FileWordsParsers
{
    public static class WordsParser
    {
        public static IFileWordsParser GetParser(string extension)
        {
            switch (extension)
            {
                default:
                    return new TxtWordParser();
                case ".doc":
                case ".docx":
                    return new DocWordParser();
            }
        }
    }
}