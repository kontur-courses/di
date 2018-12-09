using System.Collections.Generic;
using System.Linq;

namespace TagsCloud
{
    public class LowerWord
    {
        private readonly IWordCollection words;

        public LowerWord(IWordCollection words)
        {
            this.words = words;
        }

        public IEnumerable<string> ToLower()
        {
            var enumerable = words.GetWords();
            return enumerable.Select(word => word.ToLowerInvariant());
        }
    }
}