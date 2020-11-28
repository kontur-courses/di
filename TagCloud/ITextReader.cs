namespace TagCloud
{
    internal interface ITextReader
    {
        public string Extension { get; }

        public string ReadText(string path);
    }
}
