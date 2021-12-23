using ResultProject;

namespace TagsCloudVisualization.WordProcessors
{
    public class LowerCaseWordProcessor : IWordProcessor
    {
        public Result<string> ProcessWord(string word)
        {
            return word.ToLower();
        }
    }
}