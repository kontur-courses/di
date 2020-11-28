namespace TagCloud
{
    public interface IWordParser
    {
        //TODO: Add OtherParsers
        string[] GetWords(string fileName);
    }
}