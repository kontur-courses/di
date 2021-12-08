using System.Linq;

namespace CloudTagContainer
{
    public class ToLowerPreprocessor: IWordsPreprocessor
    {
        public string[] Preprocess(string[] rawWords)
        {
            return rawWords
                .Select(word => word.ToLower())
                .ToArray();
        }
    }
}