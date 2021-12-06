using System.Collections.Generic;
using System.Linq;
using TagCloud.configurations;
using TagCloud.file_readers;

namespace TagCloud.handlers
{
    public class WordRepository
    {
        public readonly IEnumerable<string> words;
            
        public WordRepository(IFileReader reader, IWordFilterConfiguration configuration)
        {
            words = configuration.Filter(reader.GetWords()).Where(x => x.Length > 0);
        }
    }
}