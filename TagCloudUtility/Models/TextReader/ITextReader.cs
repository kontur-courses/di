namespace TagCloud.Utility.Models.TextReader
{
    public interface ITextReader
    {
        string[] ReadToEnd(string pathToWords);
    }
}