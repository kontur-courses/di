using System.Collections.Generic;
using TagCloud.file_readers;

namespace TagCloud.repositories
{
    public class WordRepository : IRepository<string>
    {
        private readonly IEnumerable<string> words;

        public WordRepository(string filename, IFileReader reader)
        {
            words = reader.GetWords(filename);
        }

        public IEnumerable<string> Get() => words;
    }
}