namespace TagsCloudVisualization.WordReaders.WordProcessors
{
    public class LowerCaseWordProcessor : IWordProcessor
    {
        public string ProcessWord(string word)
        {
            return word.ToLower();
        }
    }
}