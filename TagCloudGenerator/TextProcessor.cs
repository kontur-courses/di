namespace TagCloudGenerator
{
    public class TextProcessor : ITextProcessor
    {
        public TextProcessor() { }
      
        public virtual IEnumerable<string> ProcessText(IEnumerable<string> text)
        {                      
            foreach (string line in text)
                yield return line.ToLower();          
        }
    }
}