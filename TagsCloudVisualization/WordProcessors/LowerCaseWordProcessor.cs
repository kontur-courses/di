using ResultProject;

namespace TagsCloudVisualization.WordProcessors
{
    internal class LowerCaseWordProcessor : IWordProcessor
    {
        public Result<string> ProcessWord(string word)
        {
            return word.ToLower();
        }
    }
}