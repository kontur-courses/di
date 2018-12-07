using System.Collections.Generic;
using System.Linq;

namespace TagsCloud
{
    public class LowerWordCollection : IWordCollection
    {
        private readonly IWordCollection words;

        public LowerWordCollection(IWordCollection words)
        {
            this.words = words;
        }

        public IEnumerable<string> GetWords()
        {
            var enumerable = words.GetWords();
            return enumerable.Select(word => word.ToLowerInvariant());
        }
    }
}