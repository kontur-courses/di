using System.Collections.Generic;
using TagCloud.configurations;
using TagCloud.file_readers;

namespace TagCloud.repositories
{
    public class WordRepository : IRepository<string>
    {
        private readonly IEnumerable<string> words;
        
        public WordRepository(IFileReader reader, IWordRepositoryConfiguration repositoryConfiguration)
        {
            words = repositoryConfiguration.Filter(repositoryConfiguration.Handle(reader.GetWords()));
        }

        public IEnumerable<string> Get() => words;
    }
}