namespace TextPreprocessor.Core
{
    public struct Tag
    {
        public string Content { get; }
        public Tag(string name) => Content = name;
    }
}