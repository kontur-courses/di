using TagCloudContainer.Interfaces;

namespace TagCloudContainer.Processors
{
    public class WordProcessor : IWordProcessor
    {
        public IEnumerable<string> ApplyFunction(
            IEnumerable<string> textWords,
            Func<string, string> normalizeFunction)
        {
            return textWords.Select(normalizeFunction);
        }
    }
}
