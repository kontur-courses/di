using TagsCloud.Interfaces;
using System.Linq;

namespace TagsCloud.WordProcessing
{
    public class DefaultWordValidator : IWordValidator
    {
        private string[] boringWords = new string[] {"и", "да", "но", "а", "не", "но", "без"};

        public bool ISValidWord(string word)
        {
            if (word.Length == 0)
                return false;
            return !boringWords.Contains(word) && !int.TryParse(word, out var res);
        }
    }
}
