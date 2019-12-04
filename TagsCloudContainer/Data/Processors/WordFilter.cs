using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.Data.Readers;

namespace TagsCloudContainer.Data.Processors
{
    public class WordFilter : IWordReader
    {
        private readonly IWordReader reader;
        private readonly ISet<string> excluded;

        public WordFilter(IWordReader reader, IEnumerable<string> excluded)
        {
            this.reader = reader;
            this.excluded = new HashSet<string>(excluded);
        }

        public IEnumerable<string> ReadAllWords()
        {
            return reader.ReadAllWords().Where(word => !excluded.Contains(word));
        }
    }
}