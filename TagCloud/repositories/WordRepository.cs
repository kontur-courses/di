﻿using System.Collections.Generic;
using System.Linq;
using TagCloud.configurations;
using TagCloud.file_readers;

namespace TagCloud.repositories
{
    public class WordRepository : IRepository<string>
    {
        private readonly List<string> words;

        public WordRepository(IFileReader reader, IWordRepositoryConfiguration repositoryConfiguration)
        {
            words = repositoryConfiguration.Filter(repositoryConfiguration.Handle(reader.GetWords())).ToList();
        }

        public IEnumerable<string> Get() => words;
    }
}