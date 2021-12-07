using System.Linq;

namespace CloudTagContainer
{
    public class ToLowerPreprocessor: IWordsPreprocessor
    {
        private const int WordLowerLength = 2;
        public string[] Preprocess(string[] rawWords)
        {
            return rawWords
                .Select(word => word.ToLower())
                .Where(word => !IsBoring(word))
                .ToArray();
        }

        private bool IsBoring(string word)
        {
            return word.Length <= WordLowerLength ||
                   word.Any(x => !char.IsLetter(x) && !char.IsDigit(x));
        }
    }
}