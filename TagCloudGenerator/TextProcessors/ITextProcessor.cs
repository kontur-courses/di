namespace TagCloudGenerator.TextProcessors
{
    public interface ITextProcessor
    {
        IEnumerable<string> ProcessText(IEnumerable<string> text);
    }
}