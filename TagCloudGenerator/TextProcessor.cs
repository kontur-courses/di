namespace TagCloudGenerator
{
    public class TextProcessor : ITextProcessor
    {
        public IEnumerable<string> ProcessText(IEnumerable<string> text)
        {                      
            foreach (string line in text)
                yield return line.ToLower();          
        }
    }
}