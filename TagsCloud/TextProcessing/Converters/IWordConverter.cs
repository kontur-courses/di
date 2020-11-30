namespace TagsCloud.TextProcessing.Converters
{
    public interface IWordConverter
    {
        public string Convert(string word);

        public string Name { get; }
    }
}
