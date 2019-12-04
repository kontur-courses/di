using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.Data.Readers;

namespace TagsCloudContainer.Data.Processors
{
    public class LowerCaseTransformer : IWordReader
    {
        private readonly IWordReader reader;

        public LowerCaseTransformer(IWordReader reader)
        {
            this.reader = reader;
        }
        
        public IEnumerable<string> ReadAllWords()
        {
            return reader.ReadAllWords().Select(word => word.ToLower());
        }
    }
}