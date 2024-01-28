namespace TagCloudGenerator
{
    public interface ITextProcessor
    {
        IEnumerable<string> ProcessText(IEnumerable<string> text);
    }
}
