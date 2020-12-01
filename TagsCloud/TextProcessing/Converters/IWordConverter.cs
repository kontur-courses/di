namespace TagsCloud.TextProcessing.Converters
{
    public interface IWordConverter
    {
        string Convert(string word);

        string Name { get; }
    }
}
