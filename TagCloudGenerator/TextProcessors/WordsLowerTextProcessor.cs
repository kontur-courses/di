namespace TagCloudGenerator.TextProcessors
{
    public class WordsLowerTextProcessor : ITextProcessor
    {
        public IEnumerable<string> ProcessText(IEnumerable<string> text)
        {
            foreach (string line in text)
                yield return line.ToLower();
        }
    }
}