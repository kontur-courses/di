using System.Linq;

namespace Visualization
{
    public class ToLowerPreprocessor : IWordsPreprocessor
    {
        public string[] Preprocess(string[] rawWords)
        {
            return rawWords
                .Select(word => word.ToLower())
                .ToArray();
        }
    }
}