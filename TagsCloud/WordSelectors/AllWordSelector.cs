using System.Collections.Generic;

namespace TagsCloud.WordSelector
{
    public class AllWordSelector : IWordSelector
    {
        public IEnumerable<string> TakeSelectedWords(IEnumerable<string> words)
        {
            return words;
        }
    }
}