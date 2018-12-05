namespace TagCloud.WordsProcessors.ProcessingUtilities
{
    public class LowerCaseUtility : IProcessingUtility
    {
        public string Handle(string word)
        {
            return word.ToLower();
        }
    }
}