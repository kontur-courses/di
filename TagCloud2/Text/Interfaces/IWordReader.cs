namespace TagCloud2
{
    public interface IWordReader
    {
        string[] GetUniqueLowercaseWords(string input);
    }
}
