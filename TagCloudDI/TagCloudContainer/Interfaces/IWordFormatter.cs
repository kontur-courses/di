namespace TagCloudContainer.Interfaces
{
    public interface IWordProcessor
    {
        IEnumerable<string> ApplyFunction(IEnumerable<string> textWords, Func<string, string> normalizeFunction);
    }
}
